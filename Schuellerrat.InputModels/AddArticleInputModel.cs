namespace Schuellerrat.InputModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;

    public class AddArticleInputModel
    {
        public AddArticleInputModel()
        {
            this.Images = new List<IFormFile>();
            this.Paragraphs = new List<ParagraphInputModel>();
        }

        [MinLength(5)]
        [Required]
        public string Title { get; set; }

        [DataType(DataType.Upload)]
        public ICollection<IFormFile> Images { get; set; }

        public ICollection<ParagraphInputModel> Paragraphs { get; set; }
    }
}
