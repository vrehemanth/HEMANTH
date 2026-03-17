using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EGI_Backend.Application.DTOs;

namespace EGI_Backend.Application.Interfaces
{
    public interface IPolicyEndorsementService
    {
        Task<EndorsementResponseDto> SubmitEndorsementAsync(Guid customerId, SubmitEndorsementDto dto);
        Task<EndorsementResponseDto> ReviewEndorsementAsync(Guid userId, string role, Guid endorsementId, ReviewEndorsementDto dto);
        Task<List<EndorsementResponseDto>> GetEndorsementsByPolicyAsync(Guid policyAssignmentId, Guid userId, string role);
        Task<List<EndorsementResponseDto>> GetPendingEndorsementsAsync(Guid userId, string role);
    }
}
