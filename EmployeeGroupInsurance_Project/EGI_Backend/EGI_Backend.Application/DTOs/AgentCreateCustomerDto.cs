using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGI_Backend.Domain.Enums;

namespace EGI_Backend.Application.DTOs
{
    public class AgentCreateCustomerDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public IndustryType IndustryType { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public List<IFormFile> Documents { get; set; }
        public List<DocumentType>? DocumentTypes { get; set; }
    }
}
