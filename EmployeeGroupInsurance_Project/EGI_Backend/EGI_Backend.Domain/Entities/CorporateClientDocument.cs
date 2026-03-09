using EGI_Backend.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGI_Backend.Domain.Entities
{
    public class CorporateClientDocument
    {
        [Key]
        public required Guid Id { get; set; }
        public required Guid CorporateClientId { get; set; }
        public required DocumentType DocumentType { get; set; }
        public required string FileName { get; set; }
        public required string FilePath { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}
