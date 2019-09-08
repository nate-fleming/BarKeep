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

namespace BarKeep.Controllers
{
    [Authorize]
    public class CocktailsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CocktailsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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
                cocktails = cocktails.Where(c => c.Name.Contains(searchString)
                || c.Ingredients.Any(i => i.Name.Contains(searchString)));
            }

            return View(await cocktails.ToListAsync());
        }

        // GET: My Cocktails
        public async Task<IActionResult> MyCocktails()
        {
            var user = await GetCurrentUserAsync();
            

            var applicationDbContext = _context.Cocktail
                .Include(c => c.AlcoholType)
                .Include(c => c.Glassware)
                .Include(c => c.Ingredients)
                .Include(c => c.Instructions)
                .Include(c => c.User)
                .Where(c => c.UserId == user.Id);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Cocktails/Details/5
        public async Task<IActionResult> Details(int? id)
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
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.CocktailId == id);
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
            return View();
        }

        // POST: Cocktails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CocktailId,Name,AlcoholTypeId,Source,GlasswareId,Garnish,ImgUrl,Ingredients, Instructions")] Cocktail cocktail)
        {
            var user = await GetCurrentUserAsync();

            ModelState.Remove("UserId");
            if (ModelState.IsValid)
            {
                cocktail.UserId = user.Id;
                _context.Add(cocktail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlcoholTypeId"] = new SelectList(_context.AlcoholType, "AlcoholTypeId", "Name", cocktail.AlcoholTypeId);
            ViewData["GlasswareId"] = new SelectList(_context.Glassware, "GlasswareId", "Name", cocktail.GlasswareId);
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
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.CocktailId == id);


            if (cocktail == null)
            {
                return NotFound();
            }
            ViewData["AlcoholTypeId"] = new SelectList(_context.AlcoholType, "AlcoholTypeId", "Name", cocktail.AlcoholTypeId);
            ViewData["GlasswareId"] = new SelectList(_context.Glassware, "GlasswareId", "Name", cocktail.GlasswareId);
            return View(cocktail);
        }

        // POST: Cocktails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CocktailId,Name,AlcoholTypeId,Source,GlasswareId,Garnish,ImgUrl")] Cocktail cocktail)
        {
            if (id != cocktail.CocktailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cocktail);
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
            var cocktail = await _context.Cocktail.FindAsync(id);
            _context.Cocktail.Remove(cocktail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CocktailExists(int id)
        {
            return _context.Cocktail.Any(e => e.CocktailId == id);
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
