using System;
using System.Threading;
using System.Threading.Tasks;
using EGI_Backend.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EGI_Backend.Infrastructure.BackgroundJobs
{
    /// <summary>
    /// Runs once daily at startup and then every 24 hours.
    /// 1. Generates invoices for all active policies whose billing day matches today.
    /// 2. Marks overdue pending invoices as Overdue.
    /// </summary>
    public class InvoiceGenerationJob : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<InvoiceGenerationJob> _logger;
        private static readonly TimeSpan Interval = TimeSpan.FromHours(24);

        public InvoiceGenerationJob(IServiceScopeFactory scopeFactory, ILogger<InvoiceGenerationJob> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Invoice generation job started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var invoiceService = scope.ServiceProvider.GetRequiredService<IInvoiceService>();

                    _logger.LogInformation("[InvoiceJob] Running at {Time}", DateTime.UtcNow);

                    await invoiceService.GenerateDueInvoicesAsync();
                    await invoiceService.MarkOverdueInvoicesAsync();

                    _logger.LogInformation("[InvoiceJob] Completed successfully.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "[InvoiceJob] Error during invoice generation.");
                }

                await Task.Delay(Interval, stoppingToken);
            }
        }
    }
}
