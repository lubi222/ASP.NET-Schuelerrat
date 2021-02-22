namespace Schuellerrat.ViewModels
{
    using System.Collections.Generic;

    public class ClubViewModel
    {
        public string Title { get; set; }

        public string Leader { get; set; }

        public int? MaxClass { get; set; }

        public int? MinClass { get; set; }

        public string Time { get; set; }

        public ArticleViewModel Article { get; set; }
    }
}
