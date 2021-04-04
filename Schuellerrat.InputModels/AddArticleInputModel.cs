namespace Schuellerrat.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class AddArticleInputModel
    {
        [MinLength(5)]
        [MaxLength(40)]
        public string Title { get; set; }
    }
}
