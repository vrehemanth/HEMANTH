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

            var expiredPolicies = await dbContext.PolicyAssignments
                .Where(p => p.Status == PolicyStatus.Active && p.EndDate <= now)
                .ToListAsync();

            if (expiredPolicies.Any())
            {
                foreach (var policy in expiredPolicies)
                {
                    policy.Status = PolicyStatus.Expired;
                    _logger.LogInformation($"Policy {policy.PolicyNo} automatically expired.");
                }

                dbContext.PolicyAssignments.UpdateRange(expiredPolicies);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
