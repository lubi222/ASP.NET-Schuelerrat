using System;
using System.Collections.Generic;
using System.Text;

namespace Schuellerrat.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Link
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Path { get; set; }

        public string Description { get; set; }

    }
}
