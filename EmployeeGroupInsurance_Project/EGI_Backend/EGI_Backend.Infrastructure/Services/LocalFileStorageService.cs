using EGI_Backend.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace EGI_Backend.Infrastructure.Services
{
    public class LocalFileStorageService : IDocumentStorageService
    {
        public async Task<string> UploadAsync(IFormFile file)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid() + "_" + file.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return filePath;
        }

        public Task DeleteAsync(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            return Task.CompletedTask;
        }
    }
}
