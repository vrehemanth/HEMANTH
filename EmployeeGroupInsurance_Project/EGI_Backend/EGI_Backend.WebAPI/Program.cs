using EGI_Backend.Application.Services;
using EGI_Backend.Infrastructure.Persistence;
using EGI_Backend.WebAPI.Mapping;
using EGI_Backend.Infrastructure.Repositories;
using EGI_Backend.Infrastructure.Services;
using EGI_Backend.Infrastructure.BackgroundJobs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using EGI_Backend.WebAPI.Middlewares;
using EGI_Backend.WebAPI.Filters;
using EGI_Backend.Application.Interfaces;
namespace EGI_Backend.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
                var jwt = builder.Configuration.GetSection("JwtSettings");

                // Add services to the container.
                builder.Services.AddMemoryCache();
                builder.Services.AddHttpContextAccessor();
                builder.Services.AddDbContext<EGIDbContext>(options=>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("EGIConnection")));
                builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

                builder.Services.AddScoped<ICorporateClientRepository, CorporateClientRepository>();
                builder.Services.AddScoped<IAgentCustomerRepository, AgentCustomerRepository>();
                builder.Services.AddScoped<IPolicyAssignmentRepository, PolicyAssignmentRepository>();
                builder.Services.AddScoped<IAgentCustomerService, AgentCustomerService>();
                builder.Services.AddScoped<IPolicyAssignmentService, PolicyAssignmentService>();
                builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();  
                builder.Services.AddScoped<ICorporateDocumentRepository, CorporateDocumentRepository>();
                builder.Services.AddScoped<IDocumentStorageService, LocalFileStorageService>();
                builder.Services.AddScoped<ICorporateClientService, CorporateClientService>();
                builder.Services.AddScoped<IEmailService, EmailService>();
                builder.Services.AddScoped<INotificationService, NotificationService>();
                builder.Services.AddScoped<IInsurancePlanRepository, InsurancePlanRepository>();
                builder.Services.AddScoped<IInsurancePlanService, InsurancePlanService>();
                builder.Services.AddScoped<IClaimRepository, ClaimRepository>();
                builder.Services.AddScoped<IClaimService, ClaimService>();
                builder.Services.AddScoped<IMemberRepository, MemberRepository>();
                builder.Services.AddScoped<IDependentRepository, DependentRepository>();
                builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
                builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
                builder.Services.AddScoped<IPolicyEndorsementRepository, PolicyEndorsementRepository>();
                builder.Services.AddScoped<IPolicyEndorsementService, PolicyEndorsementService>();
                builder.Services.AddScoped<IInvoiceService, InvoiceService>();
                builder.Services.AddHostedService<EGI_Backend.Infrastructure.BackgroundServices.InsuranceAutomationWorker>();
                builder.Services.AddScoped<IAuthService, AuthService>();
                builder.Services.AddScoped<IAuditLogRepository, AuditLogRepository>();
                builder.Services.AddScoped<IAdminDashboardService, AdminDashboardService>();
                builder.Services.AddScoped<IAgentDashboardService, AgentDashboardService>();
                builder.Services.AddScoped<IClaimsOfficerDashboardService, ClaimsOfficerDashboardService>();
                builder.Services.AddScoped<ICustomerDashboardService, CustomerDashboardService>();
                builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
                builder.Services.AddScoped<IUserRepository, UserRepository>();
                builder.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = jwt["Issuer"],
                        ValidAudience = jwt["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwt["Key"]!)),

                        // Tell ASP.NET Core which claim in the JWT holds the Role
                        RoleClaimType = System.Security.Claims.ClaimTypes.Role
                    };
                });
                builder.Services.AddAuthorization();
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();
            builder.Services.AddControllers(options => 
            {
                options.Filters.Add<ApiResponseFilter>();
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "EGI API",
                    Version = "v1"
                });

                // 🔐 Add JWT Authentication to Swagger
                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token.\n\nExample: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
                });

                options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            var app = builder.Build();

            app.UseMiddleware<RequestLoggingMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseExceptionHandler();
            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Uploads")),
                RequestPath = "/uploads"
            });

            app.MapControllers();

            app.Run();
        }
    }
}
