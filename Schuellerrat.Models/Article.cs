using System;
using System.Collections.Generic;
using System.Text;

namespace Schuellerrat.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Article : Content
    {
        public Article()
        {
            this.Images = new HashSet<Image>();
            this.Paragraphs = new HashSet<Paragraph>();
        }
    }
}
