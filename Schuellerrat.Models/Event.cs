namespace Schuellerrat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Event : Content
    {
        public Event()
        {
            this.Images = new HashSet<Image>();
            this.Paragraphs = new HashSet<Paragraph>();
        }

        public DateTime EventDate { get; set; }
    }
}
