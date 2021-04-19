using System;

namespace Schuellerrat.Services
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using Data;
    using InputModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using ViewModels;

    public class ClubsService : IClubsService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ICloudinaryService cloudinaryService;
        private readonly Cloudinary cloudinary;

        public ClubsService(ApplicationDbContext dbContext, ICloudinaryService cloudinaryService, Cloudinary cloudinary)
        {
            this.dbContext = dbContext;
            this.cloudinary = cloudinary;
            this.cloudinaryService = cloudinaryService;
        }

        public ICollection<ClubViewModel> GetAll()
        {
            var dbClubs = this.dbContext.Clubs.Include(c => c.Images).ToList();
            var viewClubs = dbClubs.Select(x => new ClubViewModel
            {
                Title = x.Title,
                Leader = x.Leader,
                MaxClass = x.MaxClass,
                MinClass = x.MinClass,
                Time = x.Time,
                ImageUrl =  x.Images.Any() ? x.Images.ToList()[0].Path : this.dbContext.Images.FirstOrDefault().Path,
                ShortDescription = x.ShortDescription

            }).ToList();

            return viewClubs;
        }

        public Club GetById(int id)
        {
            return this.dbContext.Clubs.FirstOrDefault(x => x.Id == id);
        }

        public async Task AddClubAsync(AddClubInputModel input, string basePath)
        {

            var clubToAdd = new Club()
            {
                Title = input.Title,
                ShortDescription = input.Description,
                Leader = input.Leader,
                MaxClass = input.MaxClass,
                MinClass = input.MinClass,
                CreatedOn = DateTime.Now,
                Time = input.Time,
                Images = input.Cover == null ? null : await this.cloudinaryService.UploadAsync(new List<IFormFile>() { input.Cover }, basePath),
            };

            await this.dbContext.Clubs.AddAsync(clubToAdd);

            await this.dbContext.SaveChangesAsync();
        }

        public async Task EditClubAsync(EditClubInputModel input, string basePath)
        {
            var oldClub = await this.dbContext.Clubs.FirstOrDefaultAsync(c => c.Id == input.Id);

            if (input.Cover != null)
            {
                var dbImagesToDelete = dbContext.Images.Where(i => i.ClubId == input.Id).ToList();
                if (dbImagesToDelete.Any())
                {
                    await this.cloudinaryService.DeleteImagesAsync(this.cloudinary,
                        dbImagesToDelete.Select(dbi => dbi.Path).ToArray());
                    dbContext.Images.RemoveRange(dbImagesToDelete);
                    await dbContext.SaveChangesAsync();
                }


                var images = await this.cloudinaryService.UploadAsync(new List<IFormFile>() { input.Cover }, basePath);
                oldClub.Images = images;
            }

            oldClub.Title = input.Title;
            oldClub.Leader = input.Leader;
            oldClub.MaxClass = input.MaxClass;
            oldClub.MinClass = input.MinClass;
            oldClub.ShortDescription = input.Description;
            oldClub.Time = input.Time;
            await this.dbContext.SaveChangesAsync();
        }

        public ICollection<AllContentViewModel> GetClubsOnAllPage()
        {
            return this.dbContext.Clubs.Select(x => new AllContentViewModel
            {
                Id = x.Id,
                Title = x.Title,
                CreatedOn = x.CreatedOn.ToString(CultureInfo.InvariantCulture)
            }).ToList();
        }

        public async Task DeleteClubAsync(int id)
        {
            var clubToRemove = await this.dbContext.Clubs.FirstOrDefaultAsync(i => i.Id == id);

            if (clubToRemove.Images.Any())
            {
                var imagePaths = clubToRemove.Images.Select(x => x.Path).ToList();
                await this.cloudinaryService.DeleteImagesAsync(cloudinary, imagePaths.ToArray());
            }

            this.dbContext.Clubs.Remove(clubToRemove);

            await this.dbContext.SaveChangesAsync();
        }
    }
}
