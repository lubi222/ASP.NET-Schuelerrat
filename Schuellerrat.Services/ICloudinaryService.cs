using System.Collections.Generic;
using System.Threading.Tasks;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Schuellerrat.Models;

namespace Schuellerrat.Services
{
    public interface ICloudinaryService
    {
        public Task<ICollection<int>> UploadAsync(ICollection<IFormFile> files, string basePath);

        public Task DeleteImage(Cloudinary cloudinary, string name);
    }
}
