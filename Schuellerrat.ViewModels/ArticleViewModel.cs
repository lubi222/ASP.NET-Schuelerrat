namespace Schuellerrat.ViewModels
{
    using System.Collections.Generic;

    public class ArticleViewModel
    {
        public ArticleViewModel()
        {
            this.Images = new List<ImageViewModel>();
            this.Paragraphs = new List<ParagraphViewModel>();
        }
        public string Title { get; set; }

        public ICollection<ImageViewModel> Images { get; set; }

        public ICollection<ParagraphViewModel> Paragraphs { get; set; }
    }
}
