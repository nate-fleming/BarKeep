using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BarKeep.Models
{
    public class CocktailDescriptor
    {
        [Key]
        public int CocktailDescriptorId { get; set; }

        [Required]
        public int CocktailId { get; set; }

        [Required]
        public int DescriptorId { get; set; }

        public Cocktail Cocktail { get; set; }

        public Descriptor Descriptor { get; set; }
    }
}
