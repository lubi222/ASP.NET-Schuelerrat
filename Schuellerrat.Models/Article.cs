using System;
using System.Collections.Generic;
using System.Text;

namespace Schuellerrat.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Article
    {
        public Article()
        {
            this.Images = new HashSet<Image>();
            this.Paragraphs = new HashSet<Paragraph>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public ICollection<Image> Images { get; set; }

        public ICollection<Paragraph> Paragraphs { get; set; }


    }
}
