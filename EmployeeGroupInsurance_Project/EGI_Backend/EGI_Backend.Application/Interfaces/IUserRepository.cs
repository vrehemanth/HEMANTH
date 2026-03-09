using EGI_Backend.Domain.Entities;
using EGI_Backend.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGI_Backend.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task AddAsync(User user);
        Task<User?> GetByIdAsync(Guid id);
        Task UpdateAsync(User user);
        Task<List<User>> GetActiveAgentsAsync();
        Task<List<User>> GetAllByRoleAsync(UserRole role);
        Task<List<User>> GetByIdsAsync(IEnumerable<Guid> ids);
        Task<int> CountByRoleAsync(UserRole role);
    }
}
