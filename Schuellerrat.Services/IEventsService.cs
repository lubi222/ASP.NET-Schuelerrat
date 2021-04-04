namespace Schuellerrat.Services
{
    using System.Collections.Generic;
    using ViewModels;

    public interface IEventsService
    {
        public SingleEventViewModel GetSingleEvent(int id);

        public ICollection<AllEventsViewModel> GetAllEvents();
    }
}
