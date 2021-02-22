using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schuellerrat.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using ViewModels;

    public class ClubsController : Controller
    {
        private readonly IClubListService clubListService;

        public ClubsController(IClubListService clubListService)
        {
            this.clubListService = clubListService;
        }

        public IActionResult Index()
        {
            var dbClubs = clubListService.GetAll().ToList();
            var viewClubs = dbClubs.Select(x => new ClubViewModel
            {
                Title = x.Title,
                Leader = x.Leader,
                MaxClass = x.MaxClass,
                MinClass = x.MinClass,
                Time = x.Time,
                Article = new ArticleViewModel
                {
                    Title = x.Article.Title,
                    Images = x.Article.Images.Select(i => new ImageViewModel()
                    {
                        Path = i.Path
                    }).ToList(),
                    Paragraphs = x.Article.Paragraphs.Select(p => new ParagraphViewModel
                    {
                        Title = p.Title,
                        Content = p.Content
                    }).ToList(),
                }
            }).ToList();


            return this.View(viewClubs);
        }
    }
}
