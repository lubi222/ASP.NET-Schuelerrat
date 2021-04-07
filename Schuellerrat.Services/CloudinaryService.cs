﻿namespace Schuellerrat.Services
{
    using System.Linq;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;
    using Models;
    using Data;

    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary cloudinary;
        private readonly ApplicationDbContext dbContext;

        public CloudinaryService(Cloudinary cloudinary, ApplicationDbContext dbContext)
        {
            this.cloudinary = cloudinary;
            this.dbContext = dbContext;
        }

        public async Task<ICollection<Image>> UploadAsync(ICollection<IFormFile> files, string path)
        {
            ICollection<Image> images = new List<Image>();

            foreach (var file in files)
            {
                images.Add(await this.UploadImageAsync(file, path));
            }

            return images;
        }

        public async Task DeleteImageAsync(Cloudinary cloudinary, string path)
        {
            var imgName = GetImageName(path);
            var delParams = new DeletionParams(imgName);
            await cloudinary.DestroyAsync(delParams);
        }

        public async Task DeleteImagesAsync(Cloudinary cloudinary, string[] paths)
        {
            var names = paths.Select(GetImageName).ToList();

            var delParams = new DelResParams()
            {
                PublicIds = names
            };

            await cloudinary.DeleteResourcesAsync(delParams);
        }

        private async Task<Image> UploadImageAsync(IFormFile file, string path)
        {
            try
            {
                string filePath = path + file.FileName;
                byte[] destinationImage;
                Directory.CreateDirectory(path);
                await using Stream fileStream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(fileStream);
                fileStream.Close();
                destinationImage = await File.ReadAllBytesAsync(filePath);

                await using var destinationStream = new MemoryStream(destinationImage);
                string fileName = file.FileName;
                fileName = fileName.Replace("&", "And");
                fileName = fileName.Replace("#", "Hashtag");
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(fileName.Split(".")[0], destinationStream),
                    PublicId = fileName.Split(".")[0],
                };
                
                var result = await this.cloudinary.UploadAsync(uploadParams);
                
                var img = new Image
                {
                    Path = result.Url.AbsoluteUri,
                };
                File.Delete(filePath);
                return img;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        private string GetImageName(string path)
        {
            return path.Split("/").TakeLast(1).ToList()[0].Split(".").ToList()[0];
        }
    }
}
