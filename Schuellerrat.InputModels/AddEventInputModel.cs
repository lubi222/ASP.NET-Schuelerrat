namespace Schuellerrat.InputModels
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AddEventInputModel
    {
        public AddEventInputModel()
        {
            this.Images = new List<IFormFile>();
        }

        [MinLength(5)]
        [MaxLength(40)]
        [Required]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime EventDate { get; set; }

        [DataType(DataType.Upload)]
        public ICollection<IFormFile> Images { get; set; }

        public ICollection<ParagraphInputModel> Paragraphs { get; set; }
    }
}
