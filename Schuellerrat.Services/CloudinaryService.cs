using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Schuellerrat.Models;

namespace Schuellerrat.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary cloudinary;

        public CloudinaryService(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
        }

        public async Task<ICollection<Image>> UploadAsync(ICollection<IFormFile> files)
        {
            try
            {
                ICollection<Image> images = new List<Image>();

                    var memoryStream = new MemoryStream();
                    byte[] destinationImage;

                    

                    foreach (var file in files)
                    {

                        await file.CopyToAsync(memoryStream);
                        destinationImage = memoryStream.ToArray();

                        var destinationStream = new MemoryStream(destinationImage);
                        string fileName = file.FileName;
                        fileName = fileName.Replace("&", "And");
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(fileName, destinationStream),
                            PublicId = fileName,
                        };

                        var result = await this.cloudinary.UploadAsync(uploadParams);
                        images.Add(new Image
                        {
                            Path = result.Uri.AbsoluteUri,
                        });
                    }

                return images;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        public async Task DeleteImage(Cloudinary cloudinary, string name)
        {
            var delParams = new DelResParams()
            {
                PublicIds = new List<string>() { name },
                Invalidate = true,
            };

            await cloudinary.DeleteResourcesAsync(delParams);
        }

        private async Task CopyFilesAsync(StreamReader Source, StreamWriter Destination)
        {
            char[] buffer = new char[0x1000];
            int numRead;
            while ((numRead = await Source.ReadAsync(buffer, 0, buffer.Length)) != 0)
            {
                await Destination.WriteAsync(buffer, 0, numRead);
            }
        }
    }
}
