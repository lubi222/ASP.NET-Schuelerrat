using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Schuellerrat.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services;

    public class EventsController : Controller
    {
        private readonly IEventsService eventsService;

        public EventsController(IEventsService eventsService)
        {
            this.eventsService = eventsService;
        }
        public IActionResult All(int id)
        {
            var events = this.eventsService.GetEventsOnAllPage(id);
            if (events == null)
            {
                return this.NotFound();
            }
            else
            {
                return this.View(events);
            }
        }

        public IActionResult Details(int id)
        {
            var singleEvent = this.eventsService.GetSingleEvent(id);
            return this.View(singleEvent);
        }
    }
}
