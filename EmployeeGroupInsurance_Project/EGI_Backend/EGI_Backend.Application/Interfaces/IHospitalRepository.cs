using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EGI_Backend.Domain.Entities;

namespace EGI_Backend.Application.Interfaces
{
    public interface IHospitalRepository
    {
        Task<List<Hospital>> GetAllAsync();
        Task<Hospital?> GetByIdAsync(Guid id);
        Task<List<Hospital>> GetNetworkHospitalsAsync();
        Task AddAsync(Hospital hospital);
        Task UpdateAsync(Hospital hospital);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
    }
}
