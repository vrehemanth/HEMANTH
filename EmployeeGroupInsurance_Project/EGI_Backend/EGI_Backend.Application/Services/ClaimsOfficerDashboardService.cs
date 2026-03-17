using EGI_Backend.Application.DTOs;
using EGI_Backend.Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace EGI_Backend.Application.Services
{
    public class ClaimsOfficerDashboardService : IClaimsOfficerDashboardService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMemoryCache _cache;

        public ClaimsOfficerDashboardService(IServiceScopeFactory scopeFactory, IMemoryCache cache)
        {
            _scopeFactory = scopeFactory;
            _cache = cache;
        }

        public async Task<ClaimsOfficerDashboardSummaryDto> GetSummaryAsync(Guid officerId)
        {
            string cacheKey = $"ClaimsOfficerDashboard_{officerId}";
            if (_cache.TryGetValue(cacheKey, out ClaimsOfficerDashboardSummaryDto cached))
            {
                return cached;
            }

            // High-Speed Multi-Scope Parallel Execution
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
    }
}
