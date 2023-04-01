using Microsoft.AspNetCore.Hosting;
using ReservationFlight.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ReservationFlight.Domain.Common
{
    public class FileStorageService : IStorageService
    {
        private readonly string _imageContentFolder;

        public FileStorageService(IWebHostEnvironment webHostEnvironment)
        {
            _imageContentFolder = Path.Combine(webHostEnvironment.WebRootPath, Constants.IMAGE_CONTENT_FOLDER_NAME);
        }

        public string GetFileUrl(string fileName)
        {
            return $"/{Constants.IMAGE_CONTENT_FOLDER_NAME}/{fileName}";
        }

        public async Task SaveFileAsync(Stream mediaBinaryStream, string fileName)
        {
            var filePath = Path.Combine(_imageContentFolder, fileName);
            using var output = new FileStream(filePath, FileMode.Create);
            await mediaBinaryStream.CopyToAsync(output);
            mediaBinaryStream.Close();
        }

        public async Task DeleteFileAsync(string fileName)
        {
            var filePath = Path.Combine(_imageContentFolder, fileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }
    }
}
