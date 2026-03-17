using EGI_Backend.Application.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using EGI_Backend.Infrastructure.Persistence;
using EGI_Backend.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EGI_Backend.Infrastructure.BackgroundServices
{
    public class InsuranceAutomationWorker : BackgroundService
    {
        private readonly ILogger<InsuranceAutomationWorker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public InsuranceAutomationWorker(ILogger<InsuranceAutomationWorker> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Insurance Automation Worker started running.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await ProcessDailyTasksAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while running Insurance Automation Worker background tasks.");
                }

                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
        }

        private async Task ProcessDailyTasksAsync()
        {
            using var scope = _scopeFactory.CreateScope();
            var invoiceService = scope.ServiceProvider.GetRequiredService<IInvoiceService>();
            var dbContext = scope.ServiceProvider.GetRequiredService<EGIDbContext>();

            _logger.LogInformation("Running Daily Insurance Automation Tasks...");

            await invoiceService.GenerateDueInvoicesAsync();
            await invoiceService.MarkOverdueInvoicesAsync();
            await ExpireOldPoliciesAsync(dbContext);

            _logger.LogInformation("Completed Daily Insurance Automation Tasks.");
        }

        private async Task ExpireOldPoliciesAsync(EGIDbContext dbContext)
        {
            var now = DateTime.UtcNow;

            // 1. Mark as EXPIRED if past Grace Period (30 days)
            // (Expired = Dead)
            var deadPolicies = await dbContext.PolicyAssignments
                .Where(p => (p.Status == PolicyStatus.Active || p.Status == PolicyStatus.Inactive) && p.EndDate.AddDays(30) <= now)
                .ToListAsync();

            if (deadPolicies.Any())
            {
                foreach (var policy in deadPolicies)
                {
                    policy.Status = PolicyStatus.Expired;
                    _logger.LogInformation($"Policy {policy.PolicyNo} marked EXPIRED (Dead).");
                }
                dbContext.PolicyAssignments.UpdateRange(deadPolicies);
            }

            // 2. Mark as INACTIVE if past EndDate but within Grace Period
            // (Inactive = Grace Period where they can still renew)
            var gracePolicies = await dbContext.PolicyAssignments
                .Where(p => p.Status == PolicyStatus.Active && p.EndDate <= now)
                .ToListAsync();

            if (gracePolicies.Any())
            {
                foreach (var policy in gracePolicies)
                {
                    policy.Status = PolicyStatus.Inactive;
                    _logger.LogInformation($"Policy {policy.PolicyNo} set to INACTIVE (Grace Period Start).");
                }
                dbContext.PolicyAssignments.UpdateRange(gracePolicies);
            }

            if (deadPolicies.Any() || gracePolicies.Any())
            {
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
