namespace Schuellerrat.ViewModels
{
    using System.Collections.Generic;

    public class ClubViewModel
    {
        public string Title { get; set; }

        public ICollection<ImageViewModel> Images { get; set; }

        public ICollection<ParagraphViewModel> Paragraphs { get; set; }
    }
}
