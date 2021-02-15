using System;
using System.Collections.Generic;
using System.Text;
using Schuellerrat.Data;

namespace Schuellerrat.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Path { get; set; }

        public int? ArticleId { get; set; }
        public Article Article { get; set; }

        public int? EventId { get; set; }
        public Event Event { get; set; }

        public int? AchievementId { get; set; }
        public Achievement Achievement { get; set; }
    }
}
