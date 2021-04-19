namespace Schuellerrat.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Club : Content
    {
        public Club()
        {
            this.Images = new HashSet<Image>();
        }
        [Required]
        public string Leader { get; set; }
        
        public int? MaxClass { get; set; }

        public int? MinClass { get; set; }

        public string Time { get; set; }

        public string ShortDescription { get; set; }

        public ICollection<Image> Images { get; set; }
    }
}
