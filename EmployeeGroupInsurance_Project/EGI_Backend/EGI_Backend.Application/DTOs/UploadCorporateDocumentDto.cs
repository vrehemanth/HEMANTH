using EGI_Backend.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGI_Backend.Application.DTOs
{
    public class UploadCorporateDocumentDto
    {
        public DocumentType DocumentType { get; set; }
        public IFormFile File { get; set; }
        public IndustryType? IndustryType { get; set; }
    }
}
