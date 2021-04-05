namespace Schuellerrat.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using InputModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using ViewModels;

    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly IDashboardService dashboardService;
        private readonly ICloudinaryService cloudinaryService;

        public DashboardController(IDashboardService dashboardService, ICloudinaryService cloudinaryService)
        {
            this.dashboardService = dashboardService;
            this.cloudinaryService = cloudinaryService;
        }

        public IActionResult Index()
        {
            var allEvents = this.dashboardService.GetAllEvents();
            var allArticles = this.dashboardService.GetAllArticles();
            return this.View(new List<List<AllContentViewModel>>(){allEvents.ToList(), allArticles.ToList()});
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult AddEvent()
        {
            return this.View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult AddEvent(AddEventInputModel input)
        {
            this.dashboardService.AddEvent(input, this.cloudinaryService);
            return this.RedirectToAction("Index");
        }
    }
}
