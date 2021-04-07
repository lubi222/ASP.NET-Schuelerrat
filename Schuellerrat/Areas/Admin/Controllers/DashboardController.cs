using System.Globalization;

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
        public IActionResult EditEvent(int id, int bonusId)
        {
            int bonusParagraphsCount = -1;


            var oldEvent = this.eventsService.GetSingleEvent(id);

            var toBeParagraphsCount = oldEvent.ParagraphTitles.Count;
            // make all the paragraphs and then make them unique(maybe)

            #region OldParagraphLogic(=cap)

            //var paragraphDict = new Dictionary<string, string>();

            //for (int i = 0; i < oldEvent.ParagraphTitles.Count; i++)
            //{
            //    if (!paragraphDict.ContainsKey(oldEvent.ParagraphTitles.ToList()[i]))
            //    {
            //        paragraphDict.Add(oldEvent.ParagraphTitles.ToList()[i], oldEvent.ParagraphTexts.ToList()[i]);
            //    }
            //}

            //var filledParagraphs = new List<ParagraphInputModel>();

            //foreach (var (key, value) in paragraphDict)
            //{
            //    if (!filledParagraphs.Any(p => p.Title == key))
            //    {
            //        filledParagraphs.Add(new ParagraphInputModel
            //        {

            //            Title = key,
            //            Content = value
            //        });
            //    }
            //}


            // (title, (text, id))
            // Dictionary<Title, Dictionary<Text, Id>>
            //var paragraphDict = new Dictionary<string, Dictionary<string, int>>();
            //for (int i = 0; i < oldEvent.ParagraphTitles.Count; i++)
            //{
            //    if (!paragraphDict.ContainsKey(oldEvent.ParagraphTitles.ToList()[i]))
            //    {
            //        paragraphDict.Add(oldEvent.ParagraphTitles.ToList()[i], new Dictionary<string, int>(){{ oldEvent.ParagraphTexts.ToList()[i], oldEvent.ParagraphIds.ToList()[i] } });
            //        //paragraphDict[oldEvent.ParagraphTitles.ToList()[i]].Add(oldEvent.ParagraphTexts.ToList()[i], oldEvent.ParagraphIds.ToList()[i]);
            //    }
            //}


            //for (int i = 0; i < paragraphDict.Count; i++)
            //{
            //    if (!filledParagraphs.Any(p => p.Title == paragraphDict.ElementAt(i).Key))
            //    {
            //        filledParagraphs.Add(new ParagraphInputModel
            //        {
            //            Title = paragraphDict.ElementAt(i).Key,
            //            Content = paragraphDict.ElementAt(i).Value.ElementAt(i).Key,
            //            Id = paragraphDict.ElementAt(i).Value.ElementAt(i).Value,
            //        });
            //    }
            //}
            #endregion

            var filledParagraphs = new List<ParagraphInputModel>();

            for (int i = 0; i < toBeParagraphsCount; i++)
            {
                filledParagraphs.Add(new ParagraphInputModel
                {
                    Id = oldEvent.ParagraphIds.ToList()[0],
                    Title = oldEvent.ParagraphTitles.ToList()[0],
                    Content = oldEvent.ParagraphTexts.ToList()[0],
                });
            }


            var newEvent = new EditInputModel
            {
                Id = id,
                Title = oldEvent.Title,
                EventDate = DateTime.ParseExact(oldEvent.EventDate,"dd/MM/yyyy", CultureInfo.InvariantCulture),
                OldImages = oldEvent.Images.Select(x => new Image
                {
                    Path = x.Path,
                    Id = x.Id
                }).ToList(),
                BonusParagraphsCount = bonusId+1,
                Paragraphs = filledParagraphs,
                ParagraphTitles = oldEvent.ParagraphTitles,
                ParagraphTexts = oldEvent.ParagraphTexts
            };
            
            return this.View(newEvent);

        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> EditEvent(EditInputModel input, int editEventId)
        {
            input.Id = editEventId;
            if (!ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.dashboardService.EditEvent(input, this.cloudinaryService, $"{this.webHostEnvironment.WebRootPath}/img/");

            return this.RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteImage(int id, int eventId)
        {
            await this.dashboardService.DeleteImageAsync(id);
            this.TempData["message"] = "Image deleted successfully.";
            var editViewModel = this.eventsService.GetSingleEvent(eventId);
            return this.RedirectToAction("EditEvent", editViewModel);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            await this.dashboardService.DeleteEventAsync(id);
            this.TempData["message"] = "Event deleted successfully.";
            return this.RedirectToAction("Index");
        }
    }
}
