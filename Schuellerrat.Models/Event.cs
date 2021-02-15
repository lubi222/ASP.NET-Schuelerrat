using System.Collections.Generic;
using Schuellerrat.Models;

namespace Schuellerrat.Data
{
    public class Event
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ICollection<Image> Images { get; set; }

    }
}