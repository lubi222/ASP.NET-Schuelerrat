namespace Schuellerrat.ViewModels
{
    using System.Collections.Generic;

    public class SingleEventViewModel
    {
        public SingleEventViewModel()
        {
            this.ParagraphIds = new HashSet<int>();
            this.ParagraphTexts = new HashSet<string>();
            this.ParagraphTitles = new HashSet<string>();
            this.Images = new HashSet<ImageViewModel>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public ICollection<int> ParagraphIds { get; set; }

        public ICollection<string> ParagraphTitles { get; set; }

        public ICollection<string> ParagraphTexts { get; set; }

        public ICollection<ImageViewModel> Images { get; set; }

        public string EventDate { get; set; }
    }
}
