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
    }
}
