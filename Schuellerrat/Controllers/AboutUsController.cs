namespace Schuellerrat.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Services;

    public class AboutUsController : Controller
    {
        private readonly IArticlesService articlesService;

        public AboutUsController(IArticlesService articlesService)
        {
            this.articlesService = articlesService;
        }
        public async Task<IActionResult> Index()
        {
            var aboutUsArticle = await this.articlesService.GetAboutUsArticle();
            return this.View(aboutUsArticle);
        }
    }
}
