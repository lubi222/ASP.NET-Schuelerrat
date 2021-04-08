using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Schuellerrat.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Schuellerrat.Controllers
{
    using Services;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEventsService eventsService;

        public HomeController(ILogger<HomeController> logger, IEventsService eventsService)
        {
            _logger = logger;
            this.eventsService = eventsService;
        }

        public IActionResult Index()
        {
            var latestEvents = this.eventsService.GetEventsOnAllPage().OrderByDescending(x => x.EventDate).Take(3)
                .ToList();
            return View(latestEvents);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
