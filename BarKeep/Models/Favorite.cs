using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BarKeep.Models
{
    public class Favorite
    {
        [Key]
        public int FavoriteId { get; set; }

        [Required]
        public int CocktailId { get; set; }

        [Required]
        public string UserId { get; set; }

        public Cocktail Cocktail { get; set; }

        public ApplicationUser User { get; set; }
    }
}
