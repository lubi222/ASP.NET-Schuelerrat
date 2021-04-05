namespace Schuellerrat.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using InputModels;
    using ViewModels;

    public interface IDashboardService
    {
        public ICollection<AllContentViewModel> GetAllArticles();

        public ICollection<AllContentViewModel> GetAllEvents();

        public Task AddEvent(AddEventInputModel input, ICloudinaryService cloudinaryService, string basePath);

        public Task AddArticle(AddArticleInputModel input);

        public Task EditEvent(EditInputModel input, ICloudinaryService cloudinaryService, string basePath);

        public Task DeleteImage(int id);
    }
}
