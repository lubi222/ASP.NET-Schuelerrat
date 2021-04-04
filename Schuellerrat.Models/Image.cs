using System;
using System.Collections.Generic;
using System.Text;

namespace Schuellerrat.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Image
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Path { get; set; }

        public int? ArticleId { get; set; }

        public Article Article { get; set; }

        public int? EventId { get; set; }

        public Event Event { get; set; }
    }
}
