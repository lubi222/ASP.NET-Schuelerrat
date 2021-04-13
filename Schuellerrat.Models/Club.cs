namespace Schuellerrat.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Club
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Leader { get; set; }
        
        public int? MaxClass { get; set; }

        public int? MinClass { get; set; }

        public string Time { get; set; }

        public string ShortDescription { get; set; }

    }
}
