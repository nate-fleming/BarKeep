using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BarKeep.Models
{
    public class AlcoholType
    {
        [Key]
        public int AlcoholTypeId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
