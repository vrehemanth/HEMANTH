using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGI_Backend.Application.Interfaces
{
    public interface IDocumentStorageService
    {
        Task<string> UploadAsync(IFormFile file);
        Task DeleteAsync(string filePath);
    }
}
