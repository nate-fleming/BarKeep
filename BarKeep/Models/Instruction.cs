using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BarKeep.Models
{
    public class Instruction
    {
        [Key]
        public int InstructionId { get; set; }

        [Required]
        public int CocktailId { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public string Description { get; set; }

    }
}
