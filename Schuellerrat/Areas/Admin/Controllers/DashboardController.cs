namespace Schuellerrat.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using InputModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using ViewModels;

    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly IDashboardService dashboardService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public DashboardController(IDashboardService dashboardService, 
            ICloudinaryService cloudinaryService,
            IWebHostEnvironment webHostEnvironment)
        {
            this.dashboardService = dashboardService;
            this.cloudinaryService = cloudinaryService;
            this.webHostEnvironment = webHostEnvironment;
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
        public async Task<IActionResult> AddEvent(AddEventInputModel input)
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("error");
            }

            try
            {
                await this.dashboardService.AddEvent(input, this.cloudinaryService, $"{this.webHostEnvironment.WebRootPath}/img/");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
            return this.RedirectToAction("Index");
        }
    }
}
