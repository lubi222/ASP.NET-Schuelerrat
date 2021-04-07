namespace Schuellerrat.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using InputModels;
    using ViewModels;

    public interface IEventsService
    {
        public SingleEventViewModel GetSingleEvent(int id);

        public ICollection<AllEventsViewModel> GetEventsOnAllPage();

        public ICollection<AllContentViewModel> GetEventsOnAdminPage();

        public Task AddEvent(AddEventInputModel input, string basePath);

        public Task EditEvent(EditEventInputModel input, string basePath);

        public Task DeleteEventAsync(int id);
    }
}
