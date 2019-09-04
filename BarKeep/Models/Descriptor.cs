using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BarKeep.Models
{
    public class Descriptor
    {
        [Key]
        public int DescriptorId { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
