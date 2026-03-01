using System;
using System.Threading.Tasks;
using EGI_Backend.Domain.Entities;

namespace EGI_Backend.Application.Interfaces
{
    public interface IDependentRepository
    {
        Task<Dependent?> GetByIdAsync(Guid id);
        Task AddAsync(Dependent dependent);
        Task UpdateAsync(Dependent dependent);
    }
}
