using System;

namespace EGI_Backend.Application.DTOs
{
    public class CorporateClientDocumentDto
    {
        public Guid Id { get; set; }
        public string DocumentType { get; set; }
        public string FileUrl { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}
