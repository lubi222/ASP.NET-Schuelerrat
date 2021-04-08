namespace Schuellerrat.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Models;

    public class AllEventsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Day { get; set; }

        public string Month { get; set; }

        public DateTime EventDate { get; set; }

        public Image Cover { get; set; }
        
        public string ShortDescription { get; set; }
    }
}
