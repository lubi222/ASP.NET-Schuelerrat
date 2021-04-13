namespace Schuellerrat.InputModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;
    using Models;

    public class EditClubInputModel
    {
        public int Id { get; set; }

        [MinLength(5)]
        [Required]
        public string Title { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile Cover { get; set; }

        public string Leader { get; set; }

        public int? MaxClass { get; set; }

        public int? MinClass { get; set; }

        public string Time { get; set; }

        public string Description { get; set; }
    }
}
