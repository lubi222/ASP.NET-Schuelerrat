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
            var viewClubs = clubListService.GetAll().ToList();
            return this.View(viewClubs);
        }
    }
}
