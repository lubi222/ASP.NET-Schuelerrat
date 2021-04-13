namespace Schuellerrat.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class ParagraphInputModel
    {
        public int Id { get; set; }

        [MinLength(1)]
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
