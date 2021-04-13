namespace Schuellerrat.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using InputModels;
    using ViewModels;

    public interface IArticlesService
    {
        public ICollection<AllContentViewModel> GetArticlesOnAdminPage();

        public Task AddArticle(AddArticleInputModel input, string basePath);

        public Task EditArticle(EditArticleInputModel input, string basePath);

        public Task DeleteArticleAsync(int id);

        public SingleArticleViewModel GetSingleArticle(int id);

        public Task<ArticleViewModel> GetAboutUsArticle();

        public Task<ArticleViewModel> GetSchoolInfo();
    }
}
