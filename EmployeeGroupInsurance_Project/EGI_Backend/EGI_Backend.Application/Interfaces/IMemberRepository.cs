using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EGI_Backend.Domain.Entities;

namespace EGI_Backend.Application.Interfaces
{
    public interface IMemberRepository
    {
        Task<Member?> GetByIdWithPolicyAsync(Guid memberId);
        Task<Member?> GetByIdAsync(Guid memberId);
        Task<List<Member>> GetByPolicyAssignmentIdAsync(Guid policyAssignmentId);
        Task AddAsync(Member member);
        Task UpdateAsync(Member member);
        Task<int> CountByClientIdAsync(Guid clientId);
        Task<int> CountActiveAsync();
        Task<int> CountActiveDependentsAsync();
        Task<List<Member>> GetByClientIdAsync(Guid clientId);
        Task<Member?> GetByEmployeeCodeAndClientAsync(string employeeCode, Guid clientId);
        Task<Member?> SearchAsync(string identifier);
        Task<Dependent?> SearchDependentAsync(string identifier);
    }
}
