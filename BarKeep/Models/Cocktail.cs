using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BarKeep.Models
{
    public class Cocktail
    {
        [Key]
        public int CocktailId { get; set; }

        [Required]
        public int Name { get; set; }

        [Required]
        public int AlcoholTypeId { get; set; }

        public string Source { get; set; }

        [Required]
        public int GlasswareId { get; set; }

        public string Garnish { get; set; }

        public string ImgUrl { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }

        public virtual ICollection<Instruction> Instructions { get; set; }

        public Alcoholtype AlcoholType { get; set; }

        public Glassware Glassware { get; set; }
    }
}
