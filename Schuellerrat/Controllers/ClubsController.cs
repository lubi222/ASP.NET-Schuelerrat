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
        private readonly IClubsService clubListService;

        public ClubsController(IClubsService clubListService)
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
                ShortDescription = x.ShortDescription
                
            }).ToList();


            return this.View(viewClubs);
        }
    }
}
