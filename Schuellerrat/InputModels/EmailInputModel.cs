using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Schuellerrat.InputModels
{
    public class EmailInputModel
    {
        [Required]
        public string Name { get; set; }

        [Range(8,12)]
        public int? Grade { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
