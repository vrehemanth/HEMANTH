using EGI_Backend.Application.Interfaces;
using EGI_Backend.Application.Services;
using EGI_Backend.Infrastructure.Persistence;
using EGI_Backend.Infrastructure.Repositories;
using EGI_Backend.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace EGI_Backend.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EGIDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("EGIConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Repositories
            services.AddScoped<ICorporateClientRepository, CorporateClientRepository>();
            services.AddScoped<IAgentCustomerRepository, AgentCustomerRepository>();
            services.AddScoped<IPolicyAssignmentRepository, PolicyAssignmentRepository>();
            services.AddScoped<ICorporateDocumentRepository, CorporateDocumentRepository>();
            services.AddScoped<IInsurancePlanRepository, InsurancePlanRepository>();
            services.AddScoped<IClaimRepository, ClaimRepository>();
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IDependentRepository, DependentRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPolicyEndorsementRepository, PolicyEndorsementRepository>();
            services.AddScoped<IAuditLogRepository, AuditLogRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            // Infrastructure Services
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IDocumentStorageService, LocalFileStorageService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IOCRService, OCRService>();
            services.AddScoped<IFraudDetectionService, FraudDetectionService>();
            services.AddHostedService<EGI_Backend.Infrastructure.BackgroundServices.InsuranceAutomationWorker>();

            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAgentCustomerService, AgentCustomerService>();
            services.AddScoped<IPolicyAssignmentService, PolicyAssignmentService>();
            services.AddScoped<ICorporateClientService, CorporateClientService>();
            services.AddScoped<IInsurancePlanService, InsurancePlanService>();
            services.AddScoped<IClaimService, ClaimService>();
            services.AddScoped<IPolicyEndorsementService, PolicyEndorsementService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAdminDashboardService, AdminDashboardService>();
            services.AddScoped<IAgentDashboardService, AgentDashboardService>();
            services.AddScoped<IClaimsOfficerDashboardService, ClaimsOfficerDashboardService>();
            services.AddScoped<ICustomerDashboardService, CustomerDashboardService>();

            return services;
        }
    }
}
