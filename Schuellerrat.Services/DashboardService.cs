using Microsoft.EntityFrameworkCore;

namespace Schuellerrat.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using Data;
    using InputModels;
    using Models;
    using ViewModels;

    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ICloudinaryService cloudinaryService;
        private readonly Cloudinary cloudinary;

        public DashboardService(ApplicationDbContext dbContext, ICloudinaryService cloudinaryService, Cloudinary cloudinary)
        {
            this.dbContext = dbContext;
            this.cloudinaryService = cloudinaryService;
            this.cloudinary = cloudinary;
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

            oldEvent.Title = input.Title;
            oldEvent.EventDate = input.EventDate;
            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteImageAsync(int id)
        {
            var imageToRemove = await this.dbContext.Images.FirstOrDefaultAsync(i => i.Id == id);
            this.dbContext.Images.Remove(imageToRemove);
            await this.cloudinaryService.DeleteImageAsync(cloudinary, imageToRemove.Path);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteEventAsync(int id)
        {
            var eventToRemove = await this.dbContext.Events.Include(x => x.Images).FirstOrDefaultAsync(i => i.Id == id);
            var imagePaths = eventToRemove.Images.Select(x => x.Path).ToArray();

            await this.cloudinaryService.DeleteImagesAsync(cloudinary, imagePaths);
            this.dbContext.Events.Remove(eventToRemove);

            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteParagraphAsync(int id)
        {
            var paragraphToRemove = await this.dbContext.Paragraphs.FirstOrDefaultAsync(p => p.Id == id);
            this.dbContext.Paragraphs.Remove(paragraphToRemove);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
