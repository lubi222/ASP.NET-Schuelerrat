namespace Schuellerrat.InputModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;

    public class AddClubInputModel
    {
        public AddClubInputModel()
        {
            this.Images = new List<IFormFile>();
        }

        [MinLength(5)]
        [MaxLength(40)]
        [Required]
        public string Title { get; set; }

        public string Leader { get; set; }

        public int? MaxClass { get; set; }

        public int? MinClass { get; set; }

        public string Time { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Upload)]
        public ICollection<IFormFile> Images { get; set; }


    }
}
