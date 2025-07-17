using UrbaPark.Dominio.Servicio.Abstracciones;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace UrbaPark.Dominio.Servicio.Implementaciones
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileStorageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> SaveFileAsync(byte[] fileData, string fileName, string folderName)
        {
            var webRootPath = _webHostEnvironment.WebRootPath;
            if (string.IsNullOrEmpty(webRootPath))
            {
                webRootPath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot");
            }

            if (!Directory.Exists(webRootPath))
            {
                Directory.CreateDirectory(webRootPath);
            }

            var uploadsFolder = Path.Combine(webRootPath, folderName);
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            await File.WriteAllBytesAsync(filePath, fileData);

            return $"/{folderName}/{uniqueFileName}";
        }

        public void DeleteFile(string filePath, string folderName)
        {
            var webRootPath = _webHostEnvironment.WebRootPath;
            if (!Directory.Exists(webRootPath))
            {
                Directory.CreateDirectory(webRootPath);
            }

            var fullPath = Path.Combine(webRootPath, filePath.TrimStart('/'));
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}