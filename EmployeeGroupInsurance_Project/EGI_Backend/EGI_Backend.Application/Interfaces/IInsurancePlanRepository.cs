using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EGI_Backend.Domain.Entities;

namespace EGI_Backend.Application.Interfaces
{
    public interface IInsurancePlanRepository
    {
        Task<List<InsurancePlan>> GetAllAsync();
        Task<InsurancePlan?> GetByIdAsync(Guid id);
        Task<InsurancePlan?> GetByPlanCodeAsync(string planCode);
        Task AddAsync(InsurancePlan plan);
        void Update(InsurancePlan plan);
        void Delete(InsurancePlan plan);
    }
}
