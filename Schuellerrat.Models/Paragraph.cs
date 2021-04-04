namespace Schuellerrat.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Paragraph
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }

        public int? ArticleId { get; set; }

        public Article Article { get; set; }

        public int? EventId { get; set; }

        public Event Event { get; set; }
    }
}
