using AutoMapper;
using EGI_Backend.Application.DTOs;
using EGI_Backend.Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EGI_Backend.Application.Services
{
    public class ClaimsOfficerDashboardService : IClaimsOfficerDashboardService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;

        public ClaimsOfficerDashboardService(IServiceScopeFactory scopeFactory, IMemoryCache cache, IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _cache = cache;
            _mapper = mapper;
        }

        public async Task<ClaimsOfficerDashboardSummaryDto> GetSummaryAsync(Guid officerId)
        {
            string cacheKey = $"ClaimsOfficerDashboard_{officerId}";
            if (_cache.TryGetValue(cacheKey, out ClaimsOfficerDashboardSummaryDto cached))
            {
                return cached;
            }

            var pendingTask = Task.Run(async () => {
                using var scope = _scopeFactory.CreateScope();
                return await scope.ServiceProvider.GetRequiredService<IClaimRepository>().CountPendingAsync();
            });

            var reviewedByMeTask = Task.Run(async () => {
                using var scope = _scopeFactory.CreateScope();
                return await scope.ServiceProvider.GetRequiredService<IClaimRepository>().CountReviewedByOfficerAsync(officerId);
            });

            var approvedCountTask = Task.Run(async () => {
                using var scope = _scopeFactory.CreateScope();
                return await scope.ServiceProvider.GetRequiredService<IClaimRepository>().CountApprovedByOfficerAsync(officerId);
            });

            var rejectedCountTask = Task.Run(async () => {
                using var scope = _scopeFactory.CreateScope();
                return await scope.ServiceProvider.GetRequiredService<IClaimRepository>().CountRejectedByOfficerAsync(officerId);
            });

            var approvedAmtTask = Task.Run(async () => {
                using var scope = _scopeFactory.CreateScope();
                return await scope.ServiceProvider.GetRequiredService<IClaimRepository>().GetTotalApprovedAmountByOfficerAsync(officerId);
            });

            var avgTimeTask = Task.Run(async () => {
                using var scope = _scopeFactory.CreateScope();
                return await scope.ServiceProvider.GetRequiredService<IClaimRepository>().GetAverageProcessingTimeDaysAsync();
            });

            await Task.WhenAll(pendingTask, reviewedByMeTask, approvedCountTask, rejectedCountTask, approvedAmtTask, avgTimeTask);

            var summary = new ClaimsOfficerDashboardSummaryDto
            {
                TotalPendingClaimsInSystem = pendingTask.Result,
                ClaimsReviewedByMe = reviewedByMeTask.Result,
                ApprovedAmountByMe = approvedAmtTask.Result,
                
                PendingClaimsCount = pendingTask.Result,
                ApprovedClaimsCount = approvedCountTask.Result,
                RejectedClaimsCount = rejectedCountTask.Result,
                AverageProcessingTimeDays = avgTimeTask.Result
            };

            _cache.Set(cacheKey, summary, TimeSpan.FromMinutes(3));
            return summary;
        }

        public async Task<List<CorporateClientResponseDto>> GetPendingHealthCheckupsAsync()
        {
            using var scope = _scopeFactory.CreateScope();
            var repo = scope.ServiceProvider.GetRequiredService<ICorporateClientRepository>();
            var hospitalRepo = scope.ServiceProvider.GetRequiredService<IHospitalRepository>();
            
            var allClients = await repo.GetAllAsync();
            var allHistory = allClients.Where(c => c.LastHealthCheckupDate.HasValue).ToList();
            
            var dtos = _mapper.Map<List<CorporateClientResponseDto>>(allHistory);
            
            foreach (var dto in dtos)
            {
                if (dto.HealthCheckupHospitalId.HasValue)
                {
                    var h = await hospitalRepo.GetByIdAsync(dto.HealthCheckupHospitalId.Value);
                    dto.HealthCheckupHospitalName = h?.Name;
                }
                
                // DATA RECOVERY: If verifiedAt is null but counts exist, use activation date as fallback for visibility
                if (!dto.HealthCheckupVerifiedAt.HasValue && dto.LastHealthCheckupDate.HasValue && (dto.HealthCheckupActualMemberCount > 0 || dto.HealthCheckupActualDependentCount > 0))
                {
                    dto.HealthCheckupVerifiedAt = dto.LastHealthCheckupDate;
                }
            }
            
            return dtos;
        }

        public async Task<bool> UpdateHealthCheckupActualsAsync(Guid corporateClientId, int memberCount, int dependentCount)
        {
            using var scope = _scopeFactory.CreateScope();
            var repo = scope.ServiceProvider.GetRequiredService<ICorporateClientRepository>();
            var client = await repo.GetByIdAsync(corporateClientId);
            
            if (client == null) return false;
            
            // 7-day edit rule
            if (client.HealthCheckupVerifiedAt.HasValue && (DateTime.UtcNow - client.HealthCheckupVerifiedAt.Value).TotalDays > 7)
            {
                return false; // Edit window closed
            }
 
            client.HealthCheckupActualMemberCount = memberCount;
            client.HealthCheckupActualDependentCount = dependentCount;
            
            if (client.IsHealthCheckupClaimPending)
            {
               client.IsHealthCheckupClaimPending = false; // Resolved
               client.HealthCheckupVerifiedAt = DateTime.UtcNow; // Set first-time verification timestamp
            }
 
            await repo.SaveChangesAsync();
            return true;
        }
    }
}
