using System.Collections.Generic;
using System.Threading.Tasks;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Schuellerrat.Models;

namespace Schuellerrat.Services
{
    public interface ICloudinaryService
    {
        public Task<ICollection<Image>> UploadAsync(ICollection<IFormFile> files, string basePath);

        public Task DeleteImageAsync(Cloudinary cloudinary, string path);

        public Task DeleteImagesAsync(Cloudinary cloudinary, string[] paths);
    }
}
