using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;
using Schuellerrat.InputModels;

namespace Schuellerrat.ViewModels
{
    public class EditInputModel
    {
        public EditInputModel()
        {
            this.Images = new List<IFormFile>();
        }

        public int Id { get; set; }

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
