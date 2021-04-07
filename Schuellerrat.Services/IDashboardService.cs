namespace Schuellerrat.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using InputModels;
    using ViewModels;

    public interface IDashboardService
    {
        public Task DeleteImageAsync(int id);

        public Task DeleteParagraphAsync(int id);
    }
}
