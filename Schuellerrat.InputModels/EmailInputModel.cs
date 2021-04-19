using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Schuellerrat.InputModels
{
    public class EmailInputModel
    {
        [DisplayName("Име")]
        public string Name { get; set; }

        [DisplayName("Клас")]

        [Range(8, 12)]
        public int? Grade { get; set; }

        [Required]
        [DisplayName("Съдържание")]

        public string Content { get; set; }

        [Required]
        [EmailAddress]
        [DisplayName("И-мейл")]

        public string Email { get; set; }
    }
}