namespace Schuellerrat.Services
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using ViewModels;

    public class EventsService : IEventsService
    {
        private readonly ApplicationDbContext dbContext;

        public EventsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public SingleEventViewModel GetSingleEvent(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            return this.dbContext
                .Events
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new SingleEventViewModel
                {
                    Title = x.Title,
                    ParagraphTitles = x.Paragraphs.Select(pti => pti.Title).ToList(),
                    ParagraphTexts = x.Paragraphs.Select(pte => pte.Text).ToList(),
                    Images = x.Images.Select(i => i.Path).ToList(),
                    EventDate = x.EventDate.ToString("dd/MM/yyyy")
                }).FirstOrDefault();
        }

        ICollection<AllEventsViewModel> IEventsService.GetAllEvents()
        {
            return this.dbContext
                .Events
                .Select(x => new AllEventsViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Day = x.EventDate.Day,
                    Month = x.EventDate.Month.ToString(CultureInfo.CreateSpecificCulture("bg-BG").DateTimeFormat.AbbreviatedMonthNames[x.EventDate.Month - 1]),
                    ShortDescription = x.Paragraphs.Any() != true ? null : string.Join(" ", x.Paragraphs.FirstOrDefault().Text.Split().Take(15)) + "..."
                }).ToList();
        }
    }
}
