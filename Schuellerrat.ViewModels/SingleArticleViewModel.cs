namespace Schuellerrat.ViewModels
{
    using System.Collections.Generic;

    public class SingleArticleViewModel : SingleContentViewModel
    {
        public SingleArticleViewModel()
        {
            this.ParagraphIds = new HashSet<int>();
            this.ParagraphTexts = new HashSet<string>();
            this.ParagraphTitles = new HashSet<string>();
            this.Images = new HashSet<ImageViewModel>();
        }
    }
}
