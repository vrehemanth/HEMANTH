using EGI_Backend.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace EGI_Backend.Infrastructure.Services
{
    public class LocalFileStorageService : IDocumentStorageService
    {
        public async Task<string> UploadAsync(IFormFile file)
        {
            // 1. Extension Whitelist (Crucial for Security)
            var permittedExtensions = new[] { ".pdf", ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
            {
                throw new InvalidOperationException("Security Error: Only .pdf, .jpg, and .png files are permitted for medical records.");
            }

            // 2. Folder Isolation
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

            // 3. Filename Hardening (No user-provided components in the final disk path)
            var uniqueFileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // 4. Secure Streaming
            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
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
