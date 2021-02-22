namespace Schuellerrat.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Club
    {
        public Club()
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

        // Otgovornik

        // Maxclass, minclass

        // TODO: vremetraene (later) - string? // x puti v sedmicata

    }
}
