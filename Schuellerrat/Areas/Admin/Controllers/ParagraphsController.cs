
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schuellerrat.InputModels;
using Schuellerrat.Services;

namespace Schuellerrat.Areas.Admin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParagraphsController : Controller
    {
        private readonly IDashboardService dashboardService;

        public ParagraphsController(IDashboardService dashboardService)
        {
            this.dashboardService = dashboardService;
        }

        [HttpPost]
        public async Task Paragraphs(DeleteParagraphInputModel input)
        {
            await this.dashboardService.DeleteParagraphAsync(input.Id);
        }
    }
}
