namespace Schuellerrat.Services
{
    using System.Collections.Generic;
    using ViewModels;

    public interface IDashboardService
    {
        public ICollection<AllContentViewModel> GetAllArticles();

        public ICollection<AllContentViewModel> GetAllEvents();
    }
}
