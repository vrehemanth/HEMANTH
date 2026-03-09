using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EGI_Backend.Domain.Entities;
using EGI_Backend.Domain.Enums;

namespace EGI_Backend.Application.Interfaces
{
    public interface IPolicyEndorsementRepository
    {
        Task AddAsync(PolicyEndorsement endorsement);
        Task<PolicyEndorsement?> GetByIdAsync(Guid id);
        Task UpdateAsync(PolicyEndorsement endorsement);
        Task<List<PolicyEndorsement>> GetByPolicyIdAsync(Guid policyAssignmentId);
        Task<List<PolicyEndorsement>> GetPendingAsync();
        Task<List<PolicyEndorsement>> GetByClientIdAsync(Guid clientId);
    }
}
