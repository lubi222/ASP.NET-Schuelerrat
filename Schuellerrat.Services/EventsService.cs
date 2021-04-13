namespace Schuellerrat.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using Data;
    using InputModels;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using ViewModels;

    public class EventsService : IEventsService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ICloudinaryService cloudinaryService;
        private readonly Cloudinary cloudinary;
        private const int eventsPerPage = 6;

        public EventsService(ApplicationDbContext dbContext, ICloudinaryService cloudinaryService, Cloudinary cloudinary)
        {
            this.dbContext = dbContext;
            this.cloudinaryService = cloudinaryService;
            this.cloudinary = cloudinary;
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
                    Id = x.Id,
                    Title = x.Title,
                    ParagraphIds = x.Paragraphs.Select(p => p.Id).ToList(),
                    ParagraphTitles = x.Paragraphs.Select(p => p.Title).ToList(),
                    ParagraphTexts = x.Paragraphs.Select(p => p.Text).ToList(),
                    Images = x.Images.Select(i => new ImageViewModel
                    {
                        Path = i.Path,
                        Id = i.Id
                    }).ToList(),
                    EventDate = x.EventDate.ToString("dd/MM/yyyy")
                }).FirstOrDefault();
        }

        public ICollection<AllEventsViewModel> GetEventsOnAllPage(int currentPage)
        {
            var maxPages = this.GetMaxPages();
            if (currentPage <= 0 || currentPage > maxPages)
            {
                return null;
            }
            
            return this.dbContext
                .Events
                .Select(x => new AllEventsViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Day = x.EventDate.Day,
                    EventDate = x.EventDate,
                    CurrentPage = currentPage,
                    PageCount = (int)this.GetMaxPages(),
                    Cover = x.Images.Count != 0 ? x.Images.FirstOrDefault().Path : this.dbContext.Images.FirstOrDefault(i => i.Id == 1).Path,
                    Month = x.EventDate.Month.ToString(CultureInfo.CreateSpecificCulture("bg-BG").DateTimeFormat.AbbreviatedMonthNames[x.EventDate.Month - 1]),
                    ShortDescription = x.Paragraphs.Any() != true ? null : GetShortDescription(x.Paragraphs.FirstOrDefault().Text),
                }).Skip((currentPage - 1) * eventsPerPage).Take(eventsPerPage).ToList();
        }

        public ICollection<AllContentViewModel> GetEventsOnAdminPage()
        {
            return this.dbContext
                .Events
                .Select(x => new AllContentViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    CreatedOn = x.CreatedOn.ToString("dd/MM/yyyy")
                })
                .ToList();
        }

        public async Task AddEvent(AddEventInputModel input, string basePath)
        {
            await this.dbContext.Events.AddAsync(new Event
            {
                Title = input.Title,
                Paragraphs = input.Paragraphs?.Select(p => new Paragraph
                {
                    Title = p.Title,
                    Text = p.Content,
                }).ToList(),
                EventDate = input.EventDate,
                CreatedOn = DateTime.Now,
                Images = input.Images != null ? await this.cloudinaryService.UploadAsync(input.Images, basePath) : null
            });
            await this.dbContext.SaveChangesAsync();
        }

        public async Task EditEvent(EditEventInputModel input, string basePath)
        {
            var oldEvent = this.dbContext.Events.FirstOrDefault(e => e.Id == input.Id);
            var oldEventParagraphs = this.dbContext.Paragraphs.Where(p => p.EventId == oldEvent.Id);
            oldEvent.Paragraphs = oldEventParagraphs.ToList();

            oldEvent.EventDate = input.EventDate;

            if (input.Images.Any())
            {
                var images = await this.cloudinaryService.UploadAsync(input.Images, basePath);
                foreach (var img in images)
                {
                    oldEvent.Images.Add(img);
                }
            }

            foreach (var paragraph in input.Paragraphs)
            {
                if (!oldEvent.Paragraphs.Any(p => p.Title == paragraph.Title))
                {
                    oldEvent.Paragraphs.Add(new Paragraph
                    {
                        Title = paragraph.Title,
                        Text = paragraph.Content,
                        EventId = oldEvent.Id,
                    });
                }
            }

            oldEvent.Title = input.Title;
            oldEvent.EventDate = input.EventDate;
            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteEventAsync(int id)
        {
            var eventToRemove = await this.dbContext.Events.Include(x => x.Images).FirstOrDefaultAsync(i => i.Id == id);
            
            if (eventToRemove.Images.Any())
            {
                var imagePaths = eventToRemove.Images.Select(x => x.Path).ToArray();
                await this.cloudinaryService.DeleteImagesAsync(cloudinary, imagePaths);
            }

            this.dbContext.Events.Remove(eventToRemove);

            await this.dbContext.SaveChangesAsync();
        }

        private decimal GetMaxPages()
        {
            decimal maxPages = (decimal)this.dbContext.Events.Count() / eventsPerPage;
            return Math.Ceiling(maxPages);
        }

        private static string GetShortDescription(string desc)
        {
            //string tempDesc = string.Join(" ", desc.Split().Take(15)).Substring(0, 1); // TODO: change to something higher when deploying
            //string subTempDesc = tempDesc.Substring(0, tempDesc.LastIndexOf(' ') == -1 ? 2 : tempDesc.LastIndexOf(' '));
            return "testDescr";
        }
    }
}
