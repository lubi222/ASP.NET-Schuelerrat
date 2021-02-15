using System;
using System.Collections.Generic;
using System.Text;

namespace Schuellerrat.Models
{
    public class Achievement
    {
        public Achievement()
        {
            this.Images = new HashSet<Image>();
            this.Paragraphs = new HashSet<Paragraph>();
            this.Links = new HashSet<Link>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public ICollection<Image> Images { get; set; }

        public ICollection<Paragraph> Paragraphs { get; set; }

        public ICollection<Link> Links { get; set; }
    }
}
