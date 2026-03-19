using AutoMapper;
using EGI_Backend.Application.DTOs;
using EGI_Backend.Application.Interfaces;
using EGI_Backend.Domain.Entities;
using EGI_Backend.Domain.Enums;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EGI_Backend.Application.Services
{
    public class AdminDashboardService : IAdminDashboardService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ICorporateClientService _corporateService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        private const string CacheKey = "AdminDashboardSummary";

        public AdminDashboardService(
            IServiceScopeFactory scopeFactory,
            ICorporateClientService corporateService,
            IMapper mapper,
            IMemoryCache cache)
        {
            _scopeFactory = scopeFactory;
            _corporateService = corporateService;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<DashboardSummaryDto> GetSummaryAsync()
        {
            // Instant load from cache if available (5s sliding)
            if (_cache.TryGetValue(CacheKey, out DashboardSummaryDto cachedSummary))
            {
                return cachedSummary;
            }

            // TRUE PARALLELISM: Use separate scopes/contexts for each metric
            // This bypasses the thread-safety limitation of a single DbContext
            
            var agentCountTask = Task.Run(async () => {
                using var scope = _scopeFactory.CreateScope();
                return await scope.ServiceProvider.GetRequiredService<IUserRepository>().CountByRoleAsync(UserRole.Agent);
            });

            var customerCountTask = Task.Run(async () => {
                using var scope = _scopeFactory.CreateScope();
                return await scope.ServiceProvider.GetRequiredService<IUserRepository>().CountByRoleAsync(UserRole.Customer);
            });

            var officerCountTask = Task.Run(async () => {
                using var scope = _scopeFactory.CreateScope();
                return await scope.ServiceProvider.GetRequiredService<IUserRepository>().CountByRoleAsync(UserRole.ClaimsOfficer);
            });

            var pendingClientsTask = Task.Run(async () => {
                using var scope = _scopeFactory.CreateScope();
                return await scope.ServiceProvider.GetRequiredService<ICorporateClientRepository>().CountPendingAsync();
            });

            var totalClaimsTask = Task.Run(async () => {
                using var scope = _scopeFactory.CreateScope();
                return await scope.ServiceProvider.GetRequiredService<IClaimRepository>().CountAsync();
            });

            var pendingClaimsTask = Task.Run(async () => {
                using var scope = _scopeFactory.CreateScope();
                return await scope.ServiceProvider.GetRequiredService<IClaimRepository>().CountPendingAsync();
            });

            var totalPoliciesTask = Task.Run(async () => {
                using var scope = _scopeFactory.CreateScope();
                return await scope.ServiceProvider.GetRequiredService<IPolicyAssignmentRepository>().CountAsync();
            });

            var totalRevenueTask = Task.Run(async () => {
                using var scope = _scopeFactory.CreateScope();
                return await scope.ServiceProvider.GetRequiredService<IInvoiceRepository>().GetTotalRevenueAsync();
            });

            var totalPayoutsTask = Task.Run(async () => {
                using var scope = _scopeFactory.CreateScope();
                return await scope.ServiceProvider.GetRequiredService<IClaimRepository>().GetTotalPayoutsAsync();
            });

            var totalCommissionTask = Task.Run(async () => {
                using var scope = _scopeFactory.CreateScope();
                return await scope.ServiceProvider.GetRequiredService<IPolicyAssignmentRepository>().GetTotalCommissionAsync();
            });

            var approvalRateTask = Task.Run(async () => {
                using var scope = _scopeFactory.CreateScope();
                return await scope.ServiceProvider.GetRequiredService<IClaimRepository>().GetApprovalRateAsync();
            });

            var avgClaimTask = Task.Run(async () => {
                using var scope = _scopeFactory.CreateScope();
                return await scope.ServiceProvider.GetRequiredService<IClaimRepository>().GetAverageClaimAmountAsync();
            });

            var recentLogsCountTask = Task.Run(async () => {
                using var scope = _scopeFactory.CreateScope();
                return await scope.ServiceProvider.GetRequiredService<IAuditLogRepository>().CountSignificantEventsAsync(DateTime.UtcNow.AddDays(-1));
            });

            var topLogsTask = Task.Run(async () => {
                using var scope = _scopeFactory.CreateScope();
                return await scope.ServiceProvider.GetRequiredService<IAuditLogRepository>().GetTopRecentLogsAsync(5);
            });

            var salaryPayoutTask = Task.Run(async () => {
                using var scope = _scopeFactory.CreateScope();
                var officers = await scope.ServiceProvider.GetRequiredService<IUserRepository>().GetAllByRoleAsync(UserRole.ClaimsOfficer);
                decimal totalSalary = 0;
                var now = DateTime.UtcNow;

                foreach (var officer in officers)
                {
                    if (officer.SalaryLPA.HasValue && officer.SalaryLPA > 0)
                    {
                        // Salary is paid on every month 1st.
                        // Count 1sts between CreatedAt and Now.
                        var start = officer.CreatedAt;
                        int monthsPaid = ((now.Year - start.Year) * 12) + now.Month - start.Month;
                        
                        // If they joined on the 1st, they got paid that day. 
                        // If they joined after the 1st, they get paid from the NEXT 1st.
                        if (start.Day == 1) monthsPaid += 1;

                        if (monthsPaid > 0)
                        {
                            var monthlySalary = (officer.SalaryLPA.Value * 100000m) / 12m;
                            totalSalary += (monthlySalary * monthsPaid);
                        }
                    }
                }
                return totalSalary;
            });

            await Task.WhenAll(
                agentCountTask, customerCountTask, officerCountTask, pendingClientsTask,
                totalClaimsTask, pendingClaimsTask, totalPoliciesTask, totalRevenueTask,
                totalPayoutsTask, totalCommissionTask, approvalRateTask, avgClaimTask,
                recentLogsCountTask, topLogsTask, salaryPayoutTask
            );

            var summary = new DashboardSummaryDto
            {
                AgentCount = agentCountTask.Result,
                CustomerCount = customerCountTask.Result,
                ClaimsOfficerCount = officerCountTask.Result,
                PendingClientsCount = pendingClientsTask.Result,
                TotalClaimsCount = totalClaimsTask.Result,
                PendingClaimsCount = pendingClaimsTask.Result,
                TotalPoliciesCount = totalPoliciesTask.Result,
                TotalRevenue = totalRevenueTask.Result,
                TotalPayouts = totalPayoutsTask.Result,
                TotalCommissionPayouts = totalCommissionTask.Result,
                TotalSalaryPayouts = Math.Round(salaryPayoutTask.Result, 2),
                ClaimApprovalRate = approvalRateTask.Result,
                AverageClaimAmount = avgClaimTask.Result,
                RecentActivitiesCount = recentLogsCountTask.Result,
                TopRecentLogs = topLogsTask.Result.Select(l => new RecentAuditLogDto
                {
                    Action = l.Action,
                    Entity = l.EntityName,
                    Timestamp = l.Timestamp
                }).ToList()
            };


            // Process Top Agents (Sequential is fine here as it's just one part)
            using (var scope = _scopeFactory.CreateScope())
            {
                var userRepo = scope.ServiceProvider.GetRequiredService<IUserRepository>();
                var policyRepo = scope.ServiceProvider.GetRequiredService<IPolicyAssignmentRepository>();

                var agents = await userRepo.GetAllByRoleAsync(UserRole.Agent);
                if (agents.Any())
                {
                    var agentIds = agents.Select(a => a.Id).ToList();
                    var commissions = await policyRepo.GetTotalCommissionsForAgentsAsync(agentIds);
                    var policyCounts = await policyRepo.GetPolicyCountsForAgentsAsync(agentIds);

                    summary.TopAgents = agents.Select(a => new TopAgentDto
                    {
                        AgentId = a.Id,
                        Name = a.Name,
                        TotalCommission = commissions.TryGetValue(a.Id, out var val) ? val : 0m,
                        PolicyCount = policyCounts.TryGetValue(a.Id, out var count) ? count : 0
                    })
                    .OrderByDescending(x => x.TotalCommission)
                    .Take(5)
                    .ToList();
                }
            }

            _cache.Set(CacheKey, summary, TimeSpan.FromMinutes(3));
            return summary;
        }

        public async Task<List<CorporateClientResponseDto>> GetPendingClientsAsync()
        {
            using var scope = _scopeFactory.CreateScope();
            var pending = await scope.ServiceProvider.GetRequiredService<ICorporateClientRepository>().GetPendingAsync();
            return _mapper.Map<List<CorporateClientResponseDto>>(pending);
        }

        public async Task<bool> ReviewClientAsync(Guid id, Guid adminId, ReviewCorporateClientDto dto)
        {
            await _corporateService.ReviewClientAsync(id, adminId, dto);
            _cache.Remove(CacheKey);
            return true;
        }

        public async Task<List<PolicyAssignmentResponseDto>> GetAllPolicyAssignmentsAsync()
        {
            // Flaw 8 Fix: Avoid OOM by fetching a capped list or implementing pagination
            using var scope = _scopeFactory.CreateScope();
            var repo = scope.ServiceProvider.GetRequiredService<IPolicyAssignmentRepository>();
            
            // For now, capping at 500 to prevent server crash while allowing usable dashboard data
            var assignments = await repo.GetAllWithDetailsAsync();
            var capped = assignments.OrderByDescending(a => a.StartDate).Take(500).ToList();
            
            return _mapper.Map<List<PolicyAssignmentResponseDto>>(capped);
        }

        public async Task<List<ClaimResponseDto>> GetAllClaimsAsync()
        {
            // Flaw 8 Fix: Safety cap for massive claim datasets
            using var scope = _scopeFactory.CreateScope();
            var repo = scope.ServiceProvider.GetRequiredService<IClaimRepository>();
            
            var claims = await repo.GetAllAsync();
            var capped = claims.OrderByDescending(c => c.ClaimDate).Take(500).ToList();
            
            return _mapper.Map<List<ClaimResponseDto>>(capped);
        }

        public async Task<List<UserResponseDto>> GetAllStaffAsync(string role)
        {
            if (!Enum.TryParse<UserRole>(role, true, out var userRole))
                return new List<UserResponseDto>();

            using var scope = _scopeFactory.CreateScope();
            var userRepo = scope.ServiceProvider.GetRequiredService<IUserRepository>();
            var policyRepo = scope.ServiceProvider.GetRequiredService<IPolicyAssignmentRepository>();

            var staff = await userRepo.GetAllByRoleAsync(userRole);
            var dtos = _mapper.Map<List<UserResponseDto>>(staff);

            if (userRole == UserRole.Agent)
            {
                var agentIds = dtos.Select(d => d.Id).ToList();
                var commissions = await policyRepo.GetTotalCommissionsForAgentsAsync(agentIds);
                foreach (var dto in dtos)
                {
                    dto.CommissionEarned = commissions.TryGetValue(dto.Id, out var val) ? val : 0m;
                }
            }
            return dtos;
        }

        public async Task<bool> ToggleUserStatusAsync(Guid userId)
        {
            using var scope = _scopeFactory.CreateScope();
            var userRepo = scope.ServiceProvider.GetRequiredService<IUserRepository>();
            var user = await userRepo.GetByIdAsync(userId);
            if (user == null) return false;

            user.Status = user.Status == UserStatus.Active ? UserStatus.Inactive : UserStatus.Active;
            await userRepo.UpdateAsync(user);
            _cache.Remove(CacheKey);
            return true;
        }

        public async Task<List<CorporateClientResponseDto>> GetAllClientsAsync()
        {
            using var scope = _scopeFactory.CreateScope();
            var repo = scope.ServiceProvider.GetRequiredService<ICorporateClientRepository>();
            var clients = await repo.GetAllAsync();
             // Flaw 8 Fix: Final OOM safety cap for client list
            var capped = clients.OrderByDescending(c => c.CreatedAt).Take(500).ToList();
            return _mapper.Map<List<CorporateClientResponseDto>>(capped);
        }

        public async Task<List<AuditLogResponseDto>> GetAuditLogsAsync(string? userId = null, string? entityName = null)
        {
            using var scope = _scopeFactory.CreateScope();
            var auditRepo = scope.ServiceProvider.GetRequiredService<IAuditLogRepository>();
            var userRepo = scope.ServiceProvider.GetRequiredService<IUserRepository>();

            var logs = await auditRepo.GetFilteredAsync(userId, entityName);
            var dtos = _mapper.Map<List<AuditLogResponseDto>>(logs);
            
            var userGuids = dtos
                .Where(d => !string.IsNullOrEmpty(d.UserId) && Guid.TryParse(d.UserId, out _))
                .Select(d => Guid.Parse(d.UserId!))
                .Distinct()
                .ToList();

            if (userGuids.Any())
            {
                var users = await userRepo.GetByIdsAsync(userGuids);
                var userDict = users.ToDictionary(u => u.Id.ToString(), u => u.Name);
                foreach (var dto in dtos)
                {
                    if (dto.UserId != null && userDict.TryGetValue(dto.UserId, out var name))
                        dto.UserName = name;
                }
            }

            foreach (var dto in dtos)
            {
                if (dto.Action == "Unchanged")
                {
                    bool hasOld = !string.IsNullOrEmpty(dto.OldValues) && dto.OldValues != "null" && dto.OldValues != "{}";
                    bool hasNew = !string.IsNullOrEmpty(dto.NewValues) && dto.NewValues != "null" && dto.NewValues != "{}";

                    if (!hasOld && hasNew) dto.Action = "Added";
                    else if (hasOld && !hasNew) dto.Action = "Deleted";
                    else if (hasOld && hasNew) dto.Action = "Modified";
                }
            }
            return dtos;
        }
    }
}
