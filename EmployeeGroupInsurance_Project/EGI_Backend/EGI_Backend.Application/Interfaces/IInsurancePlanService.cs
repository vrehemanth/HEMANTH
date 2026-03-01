using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EGI_Backend.Application.DTOs;

namespace EGI_Backend.Application.Interfaces
{
    public interface IInsurancePlanService
    {
        Task<List<InsurancePlanDto>> GetAllPlansAsync();
        Task<InsurancePlanDto?> GetPlanByIdAsync(Guid id);
        Task<InsurancePlanDto> CreatePlanAsync(CreateInsurancePlanDto dto);
        Task<InsurancePlanDto> UpdatePlanAsync(Guid id, UpdateInsurancePlanDto dto);
        Task<bool> DeactivatePlanAsync(Guid id);
        Task<bool> DeletePlanAsync(Guid id);
    }
}
