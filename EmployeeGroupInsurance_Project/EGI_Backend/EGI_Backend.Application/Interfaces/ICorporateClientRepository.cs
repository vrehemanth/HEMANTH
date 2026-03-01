using EGI_Backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGI_Backend.Application.Interfaces
{
    public interface ICorporateClientRepository
    {
        Task<CorporateClient?> GetByUserIdAsync(Guid userId);
        Task<CorporateClient?> GetByIdAsync(Guid id);
        Task AddAsync(CorporateClient client);
        Task<List<CorporateClient>> GetPendingAsync();
        Task<int> CountPendingAsync();
        Task<List<CorporateClient>> GetAllAsync();
        Task SaveChangesAsync();
    }
}
