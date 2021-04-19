using System;
using System.Globalization;

namespace Schuellerrat.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using InputModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;
    using ViewModels;

    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly IDashboardService dashboardService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IEventsService eventsService;
        private readonly IArticlesService articlesService;
        private readonly IClubsService clubsService;

        public DashboardController(
            IDashboardService dashboardService, 
            ICloudinaryService cloudinaryService,
            IWebHostEnvironment webHostEnvironment,
            IEventsService eventsService,
            IArticlesService articlesService,
            IClubsService clubsService)
        {
            this.dashboardService = dashboardService;
            this.cloudinaryService = cloudinaryService;
            this.webHostEnvironment = webHostEnvironment;
            this.eventsService = eventsService;
            this.articlesService = articlesService;
            this.clubsService = clubsService;
        }

        public IActionResult Index()
        {
            var allEvents = this.eventsService.GetEventsOnAdminPage();
            var allArticles = this.articlesService.GetArticlesOnAdminPage();
            var allClubs = this.clubsService.GetClubsOnAllPage();
            return this.View(new List<List<AllContentViewModel>>(){allEvents.ToList(), allArticles.ToList(), allClubs.ToList()});
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
            await this.eventsService.AddEvent(input, $"{this.webHostEnvironment.WebRootPath}/img/");
            this.TempData["message"] = "Събитието е създадено успешно.";

            return this.RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult EditEvent(int id, int bonusId)
        {
            var oldEvent = this.eventsService.GetSingleEvent(id);
            var filledParagraphs = this.FillParagraphs(oldEvent.ParagraphTexts.Count, oldEvent);

            var newEvent = new EditEventInputModel
            {
                Id = id,
                Title = oldEvent.Title,
                OldImages = oldEvent.Images.Select(x => new Image
                {
                    Path = x.Path,
                    Id = x.Id
                }).ToList(),
                EventDate = DateTime.ParseExact(oldEvent.EventDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                BonusParagraphsCount = bonusId+1,
                Paragraphs = filledParagraphs,
                ParagraphTitles = oldEvent.ParagraphTitles,
                ParagraphTexts = oldEvent.ParagraphTexts
            };
            return this.View(newEvent);

        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> EditEvent(EditEventInputModel input, int editEventId)
        {
            input.Id = editEventId;
            if (!ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.eventsService.EditEvent(input, $"{this.webHostEnvironment.WebRootPath}/img/");

            this.TempData["message"] = "Събитието е променено успешно.";
            return this.RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            await this.dashboardService.DeleteImageAsync(id);
            this.TempData["message"] = "Снимката е изтрита успешно.";
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            await this.eventsService.DeleteEventAsync(id);
            this.TempData["message"] = "Събитието е изтрито успешно.";
            return this.RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult AddArticle()
        {
            return this.View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddArticle(AddArticleInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.View(input);

            }
            await this.articlesService.AddArticle(input, $"{this.webHostEnvironment.WebRootPath}/img/");
            this.TempData["message"] = "Статията е създадено успешно.";

            return this.RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult EditArticle(int id)
        {
            var oldArticle = this.articlesService.GetSingleArticle(id);
            var filledParagraphs = this.FillParagraphs(oldArticle.ParagraphIds.Count, oldArticle);

            var newArticle = new EditArticleInputModel()
            {
                Id = id,
                Title = oldArticle.Title,
                OldImages = oldArticle.Images.Select(x => new Image
                {
                    Path = x.Path,
                    Id = x.Id
                }).ToList(),
                Paragraphs = filledParagraphs,
                ParagraphTitles = oldArticle.ParagraphTitles,
                ParagraphTexts = oldArticle.ParagraphTexts
            };
            return this.View(newArticle);

        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> EditArticle(EditArticleInputModel input, int articleId)
        {
            input.Id = articleId;
            if (!ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.articlesService.EditArticle(input, $"{this.webHostEnvironment.WebRootPath}/img/");

            this.TempData["message"] = "Статията е променено успешно.";
            return this.RedirectToAction("Index");
        }

        private List<ParagraphInputModel> FillParagraphs(int existingParagraphsCount, SingleContentViewModel oldEvent)
        {
            var filled = new List<ParagraphInputModel>();
            for (int i = 0; i < existingParagraphsCount; i++)
            {
                filled.Add(new ParagraphInputModel
                {
                    Id = oldEvent.ParagraphIds.ToList()[i],
                    Title = oldEvent.ParagraphTitles.ToList()[i],
                    Content = oldEvent.ParagraphTexts.ToList()[i],
                });
            }
            return filled;
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            await this.articlesService.DeleteArticleAsync(id);
            this.TempData["message"] = "Статията е изтрито успешно.";
            return this.RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult AddClub()
        {
            return this.View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddClub(AddClubInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.View(input);

            }
            await this.clubsService.AddClubAsync(input, $"{this.webHostEnvironment.WebRootPath}/img/");
            this.TempData["message"] = "Клубът е създаден успешно.";

            return this.RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult EditClub(int id, int bonusId)
        {
            var oldClub = this.clubsService.GetById(id);

            var newEvent = new EditClubInputModel
            {
                Id = oldClub.Id,
                Title = oldClub.Title,
                Leader = oldClub.Leader,
                MaxClass = oldClub.MaxClass,
                MinClass = oldClub.MinClass,
                Time = oldClub.Time,
                Description = oldClub.ShortDescription
            };
            return this.View(newEvent);

        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> EditClub(EditClubInputModel input, int clubId)
        {
            if (!ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.clubsService.EditClubAsync(input, $"{this.webHostEnvironment.WebRootPath}/img/");

            this.TempData["message"] = "Клубът е променен успешно.";
            return this.RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            await this.clubsService.DeleteClubAsync(id);
            this.TempData["message"] = "Клубът е изтрит успешно.";
            return this.RedirectToAction("Index");
        }
    }
}
