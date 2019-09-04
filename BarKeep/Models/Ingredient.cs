using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BarKeep.Models
{
    public class Ingredient
    {
        [Key]
        public int IngredientId { get; set; }

        [Required]
        public int CocktailId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Amount { get; set; }

        public Cocktail Cocktail { get; set; }
    }
}
