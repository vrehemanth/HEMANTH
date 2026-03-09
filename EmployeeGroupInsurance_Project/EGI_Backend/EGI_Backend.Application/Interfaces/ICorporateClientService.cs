using EGI_Backend.Application.DTOs;
using EGI_Backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGI_Backend.Application.Interfaces
{
    public interface ICorporateClientService
    {
        Task CreateProfileAsync(Guid userId, CreateCorporateProfileDto dto);
        Task<string> UploadDocumentAsync(Guid userId, UploadCorporateDocumentDto dto);
        Task<List<CorporateClientResponseDto>> GetPendingClientsAsync();
        Task ReviewClientAsync(Guid clientId, Guid adminId, ReviewCorporateClientDto dto);
    }
}
