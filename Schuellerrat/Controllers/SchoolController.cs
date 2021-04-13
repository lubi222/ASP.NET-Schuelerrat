using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Schuellerrat.Controllers
{
    using Services;

    public class SchoolController : Controller
    {
        private readonly IArticlesService articlesService;

        public SchoolController(IArticlesService articlesService)
        {
            this.articlesService = articlesService;
        }

        public async Task<IActionResult> Index()
        {
            var schoolInfo = await this.articlesService.GetSchoolInfo();
            return this.View(schoolInfo);
        }
    }
}
