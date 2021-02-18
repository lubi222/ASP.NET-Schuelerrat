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
            var dbClubs = clubListService.GetAll();
            var viewClubs = dbClubs.Select(x => new ClubViewModel
            {
                Title = x.Title,
                Images = x.Images.Select(i => new ImageViewModel
                {
                    Path = i.Path
                }).ToList(),
                Paragraphs = x.Paragraphs.Select(p => new ParagraphViewModel
                {
                    Title = p.Title,
                    Content = p.Content
                }).ToList()
            }).ToList();


            return this.View(viewClubs);
        }
    }
}
