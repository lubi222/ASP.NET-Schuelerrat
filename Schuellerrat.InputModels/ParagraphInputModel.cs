namespace Schuellerrat.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class ParagraphInputModel
    {
        [MinLength(1)]
        public string Title { get; set; }

        [MinLength(10)]
        public string Content { get; set; }
    }
}
