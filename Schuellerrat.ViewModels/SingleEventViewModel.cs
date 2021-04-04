namespace Schuellerrat.ViewModels
{
    using System.Collections.Generic;

    public class SingleEventViewModel
    {
        public string Title { get; set; }

        public ICollection<string> ParagraphTitles { get; set; }

        public ICollection<string> ParagraphTexts { get; set; }

        public ICollection<string> Images { get; set; }

        public string EventDate { get; set; }
    }
}
