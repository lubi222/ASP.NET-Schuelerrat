namespace Schuellerrat.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using Data;
    using InputModels;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using ViewModels;

    public class ArticlesService : IArticlesService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ICloudinaryService cloudinaryService;
        private readonly Cloudinary cloudinary;

        public ArticlesService(ApplicationDbContext dbContext, ICloudinaryService cloudinaryService, Cloudinary cloudinary)
        {
            this.dbContext = dbContext;
            this.cloudinaryService = cloudinaryService;
            this.cloudinary = cloudinary;
        }

        public ICollection<AllContentViewModel> GetArticlesOnAdminPage()
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

        public async Task AddArticle(AddArticleInputModel input, string basePath)
        {
            await this.dbContext.Articles.AddAsync(new Article()
            {
                Title = input.Title,
                Paragraphs = input.Paragraphs?.Select(p => new Paragraph
                {
                    Title = p.Title,
                    Text = p.Content,
                }).ToList(),
                CreatedOn = DateTime.Now,
                Images = input.Images != null ? await this.cloudinaryService.UploadAsync(input.Images, basePath) : null
            });
            await this.dbContext.SaveChangesAsync();
        }

        public async Task EditArticle(EditArticleInputModel input, string basePath)
        {
            var oldArticle = this.dbContext.Articles.FirstOrDefault(e => e.Id == input.Id);
            var oldArticleParagraphs = this.dbContext.Paragraphs.Where(p => p.ArticleId == oldArticle.Id);
            oldArticle.Paragraphs = oldArticleParagraphs.ToList();

            
            if (input.Images.Any())
            {
                var images = await this.cloudinaryService.UploadAsync(input.Images, basePath);
                foreach (var img in images)
                {
                    oldArticle.Images.Add(img);
                }
            }

            foreach (var paragraph in input.Paragraphs)
            {
                if (!oldArticle.Paragraphs.Any(p => p.Title == paragraph.Title))
                {
                    oldArticle.Paragraphs.Add(new Paragraph
                    {
                        Title = paragraph.Title,
                        Text = paragraph.Content,
                        ArticleId = oldArticle.Id,
                    });
                }
            }

            oldArticle.Title = input.Title;
            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteArticleAsync(int id)
        {
            var articleToRemove = await this.dbContext.Articles.Include(x => x.Images).FirstOrDefaultAsync(i => i.Id == id);

            if (articleToRemove.Images.Any())
            {
                var imagePaths = articleToRemove.Images.Select(x => x.Path).ToArray();
                await this.cloudinaryService.DeleteImagesAsync(cloudinary, imagePaths);
            }

            this.dbContext.Articles.Remove(articleToRemove);

            await this.dbContext.SaveChangesAsync();
        }

        public SingleArticleViewModel GetSingleArticle(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            return this.dbContext
                .Articles
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new SingleArticleViewModel()
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
                }).FirstOrDefault();
        }

        public async Task<ArticleViewModel> GetAboutUsArticle()
        {
            return await this.dbContext.Articles.Where(x => x.Id == 4).Select(x => new ArticleViewModel
            {
                Title = x.Title,
                Images = x.Images.Select(i => new ImageViewModel
                {
                    Path = i.Path,
                }).ToList(),
                Paragraphs = x.Paragraphs.Select(i => new ParagraphViewModel()
                {
                    Content = i.Text,
                    Title = i.Title
                }).ToList(),
            }).FirstOrDefaultAsync();
        }
        public async Task<ArticleViewModel> GetSchoolInfo()
        {
            return await this.dbContext.Articles.Where(x => x.Id == 4).Select(x => new ArticleViewModel
            {
                Title = x.Title,
                Images = x.Images.Select(i => new ImageViewModel
                {
                    Path = i.Path,
                }).ToList(),
                Paragraphs = x.Paragraphs.Select(i => new ParagraphViewModel()
                {
                    Content = i.Text,
                    Title = i.Title
                }).ToList(),
            }).FirstOrDefaultAsync();
        }

    }
}
