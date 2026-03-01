using EGI_Backend.Application.Interfaces;
using EGI_Backend.Domain.Entities;
using EGI_Backend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using EGI_Backend.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGI_Backend.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EGIDbContext _context;

        public UserRepository(EGIDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetActiveAgentsAsync()
        {
            return await _context.Users
                .Where(u => u.Role == UserRole.Agent && u.Status == UserStatus.Active)
                .ToListAsync();
        }

        public async Task<int> CountByRoleAsync(UserRole role)
        {
            return await _context.Users.CountAsync(u => u.Role == role);
        }

        public async Task<List<User>> GetAllByRoleAsync(UserRole role)
        {
            return await _context.Users
                .Where(u => u.Role == role)
                .ToListAsync();
        }
    }
}
