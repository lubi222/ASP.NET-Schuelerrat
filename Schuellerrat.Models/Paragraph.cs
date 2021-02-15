using System;
using System.Collections.Generic;
using System.Text;

namespace Schuellerrat.Models
{
    public class Paragraph
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int ArticleId { get; set; }
        
        public Article Article { get; set; }

    }
}
