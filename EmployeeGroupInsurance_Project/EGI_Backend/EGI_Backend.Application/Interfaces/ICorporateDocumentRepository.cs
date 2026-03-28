using EGI_Backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGI_Backend.Application.Interfaces
{
    public interface ICorporateDocumentRepository
    {
        Task AddAsync(CorporateClientDocument document);
        Task<List<CorporateClientDocument>> GetByClientIdAsync(Guid clientId);
        Task DeleteAsync(Guid id);
    }
}
