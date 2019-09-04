using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BarKeep.Models
{
    public class Glassware
    {
        [Key]
        public int GlasswareId { get; set; }
        [Required]
        public string Name { get; set; }

        public string ImgUrl { get; set; }
    }
}
