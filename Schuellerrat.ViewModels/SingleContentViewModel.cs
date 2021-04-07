namespace Schuellerrat.ViewModels
{
    using System.Collections.Generic;

    public abstract class SingleContentViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public ICollection<int> ParagraphIds { get; set; }

        public ICollection<string> ParagraphTitles { get; set; }

        public ICollection<string> ParagraphTexts { get; set; }

        public ICollection<ImageViewModel> Images { get; set; }
    }
}
