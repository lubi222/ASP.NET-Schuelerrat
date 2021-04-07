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

        public async Task DeleteImageAsync(int id)
        {
            var imageToRemove = await this.dbContext.Images.FirstOrDefaultAsync(i => i.Id == id);
            this.dbContext.Images.Remove(imageToRemove);
            await this.cloudinaryService.DeleteImageAsync(cloudinary, imageToRemove.Path);
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
