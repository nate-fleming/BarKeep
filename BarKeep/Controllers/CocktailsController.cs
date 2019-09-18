using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BarKeep.Data;
using BarKeep.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using System.IO;
using Amazon.S3;
using Amazon.S3.Transfer;
using Amazon;
using BarKeep.Keys;

namespace BarKeep.Controllers
{
    [Authorize]
    public class CocktailsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _env;

        public CocktailsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IHostingEnvironment env)
        {
            _context = context;
            _userManager = userManager;
            _env = env;
        }

        // GET: Cocktails
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AlcoholTypeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "alcoholType_desc" : "";

            var cocktails = _context.Cocktail
                .Include(c => c.AlcoholType)
                .Include(c => c.Glassware)
                .Include(c => c.Ingredients)
                .Include(c => c.User)
                .AsQueryable();

            var ingredients = _context.Ingredient
                .Include(i => i.Cocktail)
                .AsQueryable();

            switch (sortOrder)
            {
                case "name_desc":
                    cocktails = cocktails.OrderBy(c => c.Name);
                    break;
                case "alcoholType_desc":
                    cocktails = cocktails.OrderBy(c => c.AlcoholType.Name);
                    break;
                default:
                    cocktails = cocktails;
                    break;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                cocktails = cocktails.Where(c => c.Name.Contains(searchString));
                if (cocktails.ToList().Count == 0 || ingredients.Any(i => i.Name.Contains(searchString)))
                {
                    cocktails = ingredients.Where(i => i.Name.Contains(searchString)).Select(i => i.Cocktail).Include(c => c.AlcoholType).Include(c => c.User);
                }
            }


            return View(await cocktails.ToListAsync());
        }

        // GET: My Cocktails
        public async Task<IActionResult> MyCocktails(string sortOrder)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AlcoholTypeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "alcoholType_desc" : "";

            var user = await GetCurrentUserAsync();

            var myCocktails = _context.Cocktail
                .Include(c => c.AlcoholType)
                .Include(c => c.Glassware)
                .Include(c => c.Ingredients)
                .Include(c => c.Instructions)
                .Include(c => c.User)
                .Where(c => c.UserId == user.Id);

            switch (sortOrder)
            {
                case "name_desc":
                    myCocktails = myCocktails.OrderBy(c => c.Name);
                    break;
                case "alcoholType_desc":
                    myCocktails = myCocktails.OrderBy(c => c.AlcoholType.Name);
                    break;
                default:
                    myCocktails = myCocktails;
                    break;
            }

            return View(await myCocktails.ToListAsync());
        }

        //GET: Favorite Cocktails

        public async Task<IActionResult> FavoriteCocktails(string sortOrder)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AlcoholTypeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "alcoholType_desc" : "";

            var user = await GetCurrentUserAsync();

            var favoriteCocktails = await _context.Favorite
                .Where(f => f.UserId == user.Id)
                .Select(cd => cd.Cocktail)
                .Include(c => c.AlcoholType)
                .Include(c => c.Glassware)
                .Include(c => c.User)
                .ToListAsync();

            switch (sortOrder)
            {
                case "name_desc":
                    favoriteCocktails = favoriteCocktails.OrderBy(c => c.Name).ToList();
                    break;
                case "alcoholType_desc":
                    favoriteCocktails = favoriteCocktails.OrderBy(c => c.AlcoholType.Name).ToList();
                    break;
                default:
                    favoriteCocktails = favoriteCocktails;
                    break;
            }

            return View(favoriteCocktails);
        }

        //GET: Questions
        public IActionResult Questions()
        {
            ViewData["AlcoholTypeId"] = new SelectList(_context.AlcoholType, "AlcoholTypeId", "Name");
            ViewData["Descriptor1Id"] = new SelectList(_context.Descriptor, "DescriptorId", "Description");
            ViewData["Descriptor2Id"] = new SelectList(_context.Descriptor, "DescriptorId", "Description");

            return View();
        }

        //GET: Suggested Cocktails
        public async Task<IActionResult> Suggestions(string sortOrder, int alcoholType, int descriptor1, int descriptor2)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AlcoholTypeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "alcoholType_desc" : "";

            var user = await GetCurrentUserAsync();


            var suggestedCocktails = await _context.CocktailDescriptor
                .Where(cd => cd.Cocktail.AlcoholTypeId == alcoholType && (cd.DescriptorId == descriptor1 || cd.DescriptorId == descriptor2))
                .Select(cd => cd.Cocktail)
                .Distinct()
                .Include(c => c.AlcoholType)
                .Include(c => c.Glassware)
                .Include(c => c.User)
                .ToListAsync();

            ViewBag.alcoholType = alcoholType;
            ViewBag.descriptor1 = descriptor1;
            ViewBag.descriptor2 = descriptor2;


            switch (sortOrder)
            {
                case "name_desc":
                    suggestedCocktails = suggestedCocktails.OrderBy(c => c.Name).ToList();
                    break;
                case "alcoholType_desc":
                    suggestedCocktails = suggestedCocktails.OrderBy(c => c.AlcoholType.Name).ToList();
                    break;
                default:
                    suggestedCocktails = suggestedCocktails;
                    break;
            }

            return View(suggestedCocktails);
        }

        // GET: Suggestion/Detail
        public async Task<IActionResult> SuggestionDetails(int? id, int alcoholType, int descriptor1, int descriptor2)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await GetCurrentUserAsync();

            var cocktail = await _context.Cocktail
                .Include(c => c.AlcoholType)
                .Include(c => c.Glassware)
                .Include(c => c.Ingredients)
                .Include(c => c.Instructions)
                .Include(c => c.User)
                .Include(c => c.CocktailDescriptors)
                .ThenInclude(d => d.Descriptor)
                .FirstOrDefaultAsync(m => m.CocktailId == id);

            cocktail.IsFavorite = _context.Favorite
                .Any(f => f.CocktailId == id && f.UserId == user.Id);

            ViewBag.alcoholType = alcoholType;
            ViewBag.descriptor1 = descriptor1;
            ViewBag.descriptor2 = descriptor2;

            if (cocktail == null)
            {
                return NotFound();
            }

            return View(cocktail);
        }

        // GET Random Drink
        public async Task<IActionResult> Random()
        {

            var cocktails = await _context.Cocktail
               .Include(c => c.AlcoholType)
               .Include(c => c.Glassware)
               .Include(c => c.Ingredients)
               .Include(c => c.Instructions)
               .Include(c => c.User)
               .Include(c => c.CocktailDescriptors)
               .ThenInclude(d => d.Descriptor)
               .ToListAsync();

            Random rnd = new Random();
            var cocktail = cocktails[rnd.Next(cocktails.Count)];

            return View(cocktail);
        }

        // Create Favorite
        public async Task<IActionResult> Favorite(int cId)
        {
            var user = await GetCurrentUserAsync();

            var favorite = new Favorite()
            {
                UserId = user.Id,
                CocktailId = cId
            };

            _context.Add(favorite);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = cId });
            //return RedirectToAction(nameof(Index));
        }

        // Remove Favorite
        public async Task<IActionResult> Unfavorite(int cId)
        {
            var user = await GetCurrentUserAsync();

            var favorite = _context.Favorite.FirstOrDefault(f => f.CocktailId == cId && f.UserId == user.Id);

            _context.Remove(favorite);
            await _context.SaveChangesAsync();

            //return RedirectToAction(nameof(Details), new { id = cId });
            return RedirectToAction(nameof(Index));
        }

        // GET: Cocktails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await GetCurrentUserAsync();

            var cocktail = await _context.Cocktail
                .Include(c => c.AlcoholType)
                .Include(c => c.Glassware)
                .Include(c => c.Ingredients)
                .Include(c => c.Instructions)
                .Include(c => c.User)
                .Include(c => c.CocktailDescriptors)
                .ThenInclude(d => d.Descriptor)
                .FirstOrDefaultAsync(m => m.CocktailId == id);

            cocktail.IsFavorite = _context.Favorite
                .Any(f => f.CocktailId == id && f.UserId == user.Id);


            if (cocktail == null)
            {
                return NotFound();
            }

            return View(cocktail);
        }

        // GET: Cocktails/Create
        public IActionResult Create()
        {
            ViewData["AlcoholTypeId"] = new SelectList(_context.AlcoholType, "AlcoholTypeId", "Name");
            ViewData["GlasswareId"] = new SelectList(_context.Glassware, "GlasswareId", "Name");
            ViewData["Descriptor1Id"] = new SelectList(_context.Descriptor, "DescriptorId", "Description");
            ViewData["Descriptor2Id"] = new SelectList(_context.Descriptor, "DescriptorId", "Description");
            return View();
        }

        // POST: Cocktails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CocktailId,Name,AlcoholTypeId,Source,GlasswareId,Garnish,Ingredients, Instructions, CocktailDescriptors")] Cocktail cocktail, IFormFile file)
        {
            var user = await GetCurrentUserAsync();



            ModelState.Remove("UserId");
            if (ModelState.IsValid)
            {
                if (file == null)
                {
                    cocktail.ImgUrl = "/images/1568384499012-00000000-ffff-ffff-ffff-ffffffffffff.jpg";
                }
                else
                {
                    try
                    {
                        //cocktail.ImgUrl = await SaveFile(file, user.Id);
                        await UploadFileToS3(file);
                        cocktail.ImgUrl = $"https://{BucketInfo.Bucket}.s3.us-east-2.amazonaws.com/{file.FileName}";
                    }
                    catch (Exception ex)
                    {
                        return NotFound();
                    }
                }
                cocktail.UserId = user.Id;
                _context.Add(cocktail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlcoholTypeId"] = new SelectList(_context.AlcoholType, "AlcoholTypeId", "Name", cocktail.AlcoholTypeId);
            ViewData["GlasswareId"] = new SelectList(_context.Glassware, "GlasswareId", "Name", cocktail.GlasswareId);
            ViewData["Descriptor1Id"] = new SelectList(_context.Descriptor, "DescriptorId", "Description", cocktail.CocktailDescriptors[0].DescriptorId);
            ViewData["Descriptor2Id"] = new SelectList(_context.Descriptor, "DescriptorId", "Description", cocktail.CocktailDescriptors[1].DescriptorId);
            return View(cocktail);
        }

        // GET: Cocktails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cocktail = await _context.Cocktail
                .Include(c => c.AlcoholType)
                .Include(c => c.Glassware)
                .Include(c => c.Ingredients)
                .Include(c => c.Instructions)
                .Include(c => c.CocktailDescriptors)
                .ThenInclude(d => d.Descriptor)
                .Include(c => c.User).FirstOrDefaultAsync(m => m.CocktailId == id);


            if (cocktail == null)
            {
                return NotFound();
            }
            ViewData["AlcoholTypeId"] = new SelectList(_context.AlcoholType, "AlcoholTypeId", "Name", cocktail.AlcoholTypeId);
            ViewData["GlasswareId"] = new SelectList(_context.Glassware, "GlasswareId", "Name", cocktail.GlasswareId);
            ViewData["Descriptor1Id"] = new SelectList(_context.Descriptor, "DescriptorId", "Description");
            ViewData["Descriptor2Id"] = new SelectList(_context.Descriptor, "DescriptorId", "Description");
            return View(cocktail);
        }

        // POST: Cocktails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CocktailId,Name,AlcoholTypeId,Source,GlasswareId,UserId,Garnish,Ingredients,Instructions,ImgUrl, CocktailDescriptors")] Cocktail cocktail, IFormFile file)
        {
            var cocktailCheck = await _context.Cocktail
                .Include(c => c.AlcoholType)
                .Include(c => c.Glassware)
                .Include(c => c.Ingredients)
                .Include(c => c.Instructions)
                .Include(c => c.CocktailDescriptors)
                .Include(c => c.User).FirstOrDefaultAsync(m => m.CocktailId == id);

            if (id != cocktail.CocktailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //this remains same
                _context.Entry(cocktailCheck).CurrentValues.SetValues(cocktail);

                // loop through exising ingredients - if match with passed in ingredient, remove the existing ingredient
                foreach (var existIngredient in cocktailCheck.Ingredients.ToList())
                {
                    if (!cocktail.Ingredients.Any(c => c.IngredientId == existIngredient.IngredientId))
                        _context.Ingredient.Remove(existIngredient);
                }

                foreach (var existInstruction in cocktailCheck.Instructions.ToList())
                {
                    if (!cocktail.Instructions.Any(c => c.InstructionId == existInstruction.InstructionId))
                        _context.Instruction.Remove(existInstruction);
                }


                foreach (var passedInIngredient in cocktail.Ingredients)
                {
                    var existingIngredient = cocktailCheck.Ingredients.Where(i => i.IngredientId == passedInIngredient.IngredientId && i.IngredientId != 0).SingleOrDefault();
                    if (existingIngredient != null)
                    {
                        _context.Entry(existingIngredient).CurrentValues.SetValues(passedInIngredient);
                    }
                    else
                    {
                        var newIngredient = new Ingredient
                        {
                            CocktailId = id,
                            Amount = passedInIngredient.Amount,
                            Name = passedInIngredient.Name
                        };
                        cocktailCheck.Ingredients.Add(newIngredient);
                    }
                }

                foreach (var passedInInstruction in cocktail.Instructions)
                {
                    var existingInstruction = cocktailCheck.Instructions.Where(i => i.InstructionId == passedInInstruction.InstructionId && i.InstructionId != 0).SingleOrDefault();
                    if (existingInstruction != null)
                    {
                        _context.Entry(existingInstruction).CurrentValues.SetValues(passedInInstruction);
                    }
                    else
                    {
                        var newInstruction = new Instruction
                        {
                            CocktailId = id,
                            Number = passedInInstruction.Number,
                            Description = passedInInstruction.Description
                        };
                        cocktailCheck.Instructions.Add(newInstruction);
                    }
                }

                foreach (var passedInDescriptor in cocktail.CocktailDescriptors)
                {
                    var existingDescriptor = cocktailCheck.CocktailDescriptors.Where(d => d.CocktailDescriptorId == passedInDescriptor.CocktailDescriptorId).SingleOrDefault();
                    if (existingDescriptor != null)
                    {
                        _context.Entry(existingDescriptor).CurrentValues.SetValues(passedInDescriptor);
                    }
                }

                try
                {
                    if(file != null)
                    {
                        await UploadFileToS3(file);
                        cocktailCheck.ImgUrl = $"https://{BucketInfo.Bucket}.s3.us-east-2.amazonaws.com/{file.FileName}";
       
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CocktailExists(cocktail.CocktailId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlcoholTypeId"] = new SelectList(_context.AlcoholType, "AlcoholTypeId", "Name", cocktail.AlcoholTypeId);
            ViewData["GlasswareId"] = new SelectList(_context.Glassware, "GlasswareId", "Name", cocktail.GlasswareId);
            ViewData["Descriptor1Id"] = new SelectList(_context.Descriptor, "DescriptorId", "Description", cocktail.CocktailDescriptors[0].DescriptorId);
            ViewData["Descriptor2Id"] = new SelectList(_context.Descriptor, "DescriptorId", "Description", cocktail.CocktailDescriptors[1].DescriptorId);
            return View(cocktail);
        }

        // GET: Cocktails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cocktail = await _context.Cocktail
                .Include(c => c.AlcoholType)
                .Include(c => c.Glassware)
                .Include(c => c.Ingredients)
                .Include(c => c.Instructions)
                .Include(c => c.CocktailDescriptors)
                .ThenInclude(d => d.Descriptor)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.CocktailId == id);

            if (cocktail == null)
            {
                return NotFound();
            }

            return View(cocktail);
        }

        // POST: Cocktails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cocktail = await _context.Cocktail.Include(c => c.CocktailDescriptors).FirstOrDefaultAsync(m => m.CocktailId == id);
            _context.Cocktail.Remove(cocktail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        private bool CocktailExists(int id)
        {
            return _context.Cocktail.Any(e => e.CocktailId == id);
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        private async Task<string> SaveFile(IFormFile file, string userId)
        {
            if (file.Length > 5242880) throw new Exception("File too large!");
            var ext = GetMimeType(file.FileName);
            if (ext == null) throw new Exception("Invalid file type");
            var epoch = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();
            var fileName = $"{epoch}-{userId}.{ext}";
            var webRoot = _env.WebRootPath;
            var absoluteFilePath = Path.Combine(
                webRoot,
                "images",
                fileName);
            string relFilePath = null;
            if (file.Length > 0)
            {
                using (var stream = new FileStream(absoluteFilePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                    relFilePath = $"~/images/{fileName}";
                };
            }
            return relFilePath;
        }

        private string GetMimeType(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            provider.TryGetContentType(fileName, out contentType);
            if (contentType == "image/jpeg") contentType = "jpg";
            else contentType = null;
            return contentType;
        }



        public async Task UploadFileToS3(IFormFile file)
        {
            using (var client = new AmazonS3Client(BucketInfo.AWSKey, BucketInfo.AWSSKey, RegionEndpoint.USEast2))
            {
                using (var newMemoryStream = new MemoryStream())
                {
                    file.CopyTo(newMemoryStream);

                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = newMemoryStream,
                        Key = file.FileName,
                        BucketName = BucketInfo.Bucket,
                        CannedACL = S3CannedACL.PublicRead
                    };

                    var fileTransferUtility = new TransferUtility(client);
                    await fileTransferUtility.UploadAsync(uploadRequest);
                }
            }
        }
    }
}
