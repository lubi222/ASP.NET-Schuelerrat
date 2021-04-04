namespace Schuellerrat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public abstract class Content
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<Image> Images { get; set; }

        public ICollection<Paragraph> Paragraphs { get; set; }
    }
}
