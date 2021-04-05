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
        private readonly IEventsService eventsService;

        public DashboardController(
            IDashboardService dashboardService, 
            ICloudinaryService cloudinaryService,
            IWebHostEnvironment webHostEnvironment,
            IEventsService eventsService)
        {
            this.dashboardService = dashboardService;
            this.cloudinaryService = cloudinaryService;
            this.webHostEnvironment = webHostEnvironment;
            this.eventsService = eventsService;
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
                return this.View(input);

            }
            await this.dashboardService.AddEvent(input, this.cloudinaryService, $"{this.webHostEnvironment.WebRootPath}/img/");
            
           
            return this.RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult EditEvent(EditModel input, int id)
        {
            input.SingleEventViewModel = this.eventsService.GetSingleEvent(id);
                
            return this.View(input);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> EditEvent(EditInputModel input, int id)
        {
            if (!ModelState.IsValid)
            {
                return this.RedirectToAction("EditEvent", new EditModel() {EditInputModel = input});
            }

            input.Id = id;

            await this.dashboardService.EditEvent(input, this.cloudinaryService, $"{this.webHostEnvironment.WebRootPath}/img/");


            return this.RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteImage(int id, int eventId)
        {
            await this.dashboardService.DeleteImage(id);
            this.TempData["message"] = "Image deleted successfully.";
            var editViewModel = this.eventsService.GetSingleEvent(eventId);
            //return this.View("EditEvent", new EditModel{SingleEventViewModel = editViewModel});
            return this.RedirectToAction("EditEvent", editViewModel);
        }
    }
}
