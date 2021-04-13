namespace Schuellerrat.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

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

        public int? ClubId { get; set; }

        [ForeignKey("ClubId")]
        public Club Club { get; set; }
    }
}
