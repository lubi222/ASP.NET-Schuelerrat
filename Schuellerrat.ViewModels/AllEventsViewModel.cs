namespace Schuellerrat.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllEventsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Day { get; set; }

        public string Month { get; set; }

        public string ShortDescription { get; set; }
    }
}
