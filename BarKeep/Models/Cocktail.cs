using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BarKeep.Models
{
    public class Cocktail
    {
        [Key]
        public int CocktailId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int AlcoholTypeId { get; set; }

        public string Source { get; set; }

        [Required]
        public int GlasswareId { get; set; }

        public string Garnish { get; set; }

        public string ImgUrl { get; set; }

        [Required]
        public string UserId { get; set; }


        public ApplicationUser User { get; set; }

        public virtual List<Ingredient> Ingredients { get; set; }

        public virtual List<Instruction> Instructions { get; set; }

        public virtual List<CocktailDescriptor> CocktailDescriptors { get; set; }


        public AlcoholType AlcoholType { get; set; }

        public Glassware Glassware { get; set; }

        [NotMapped]
        public bool IsFavorite { get; set; }
    }
}
