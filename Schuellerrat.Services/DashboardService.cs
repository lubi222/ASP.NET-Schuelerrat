using Microsoft.EntityFrameworkCore;

namespace Schuellerrat.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using InputModels;
    using Models;
    using ViewModels;

    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext dbContext;

        public DashboardService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ICollection<AllContentViewModel> GetAllArticles()
        {
            return this.dbContext
                .Articles
                .Select(x => new AllContentViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    CreatedOn = x.CreatedOn.ToString("dd/MM/yyyy")
                })
                .ToList();
        }

        public ICollection<AllContentViewModel> GetAllEvents()
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

        public async Task AddEvent(AddEventInputModel input, ICloudinaryService cloudinaryService, string basePath)
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
                Images = input.Images != null ? await cloudinaryService.UploadAsync(input.Images, basePath) : null
            });
            await this.dbContext.SaveChangesAsync();
        }

        public async Task AddArticle(AddArticleInputModel input)
        {
            throw new System.NotImplementedException();
        }

        public async Task EditEvent(EditInputModel input, ICloudinaryService cloudinaryService, string basePath)
        {
            var oldEvent = this.dbContext.Events.FirstOrDefault(e => e.Id == input.Id);
            // get the paragraphs of the old event from the database and add them to the oldevents
            var oldEventParagraphs = this.dbContext.Paragraphs.Where(p => p.EventId == oldEvent.Id);
            oldEvent.Paragraphs = oldEventParagraphs.ToList();

            oldEvent.EventDate = input.EventDate;

            var images = await cloudinaryService.UploadAsync(input.Images, basePath);
            foreach (var img in images)
            {
                oldEvent.Images.Add(img);
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

            //oldEvent.Paragraphs = input.Paragraphs.Select(p => new Paragraph
            //{
            //    Title = p.Title,
            //    Text = p.Content,
            //    EventId = oldEvent.Id,
            //}).ToList();


            oldEvent.Title = input.Title;
            oldEvent.EventDate = input.EventDate;
            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteImage(int id)
        {
            var imageToRemove = await this.dbContext.Images.FirstOrDefaultAsync(i => i.Id == id);
            this.dbContext.Images.Remove(imageToRemove);
            await dbContext.SaveChangesAsync();
        }
    }
}
