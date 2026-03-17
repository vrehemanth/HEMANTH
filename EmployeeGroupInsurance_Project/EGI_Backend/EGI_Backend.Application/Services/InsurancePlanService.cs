using AutoMapper;
using EGI_Backend.Application.DTOs;
using EGI_Backend.Application.Interfaces;
using EGI_Backend.Application.Exceptions;
using EGI_Backend.Domain.Entities;
using EGI_Backend.Domain.Enums;

namespace EGI_Backend.Application.Services
{
    public class InsurancePlanService : IInsurancePlanService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInsurancePlanRepository _insurancePlanRepository;
        private readonly IMapper _mapper;
        private readonly IAuditLogRepository _auditRepo;
        private readonly INotificationService _notificationService;
        private readonly IUserRepository _userRepo;
 
        public InsurancePlanService(
            IUnitOfWork unitOfWork, 
            IInsurancePlanRepository insurancePlanRepository, 
            IMapper mapper, 
            IAuditLogRepository auditRepo,
            INotificationService notificationService,
            IUserRepository userRepo)
        {
            _unitOfWork = unitOfWork;
            _insurancePlanRepository = insurancePlanRepository;
            _mapper = mapper;
            _auditRepo = auditRepo;
            _notificationService = notificationService;
            _userRepo = userRepo;
        }

        public async Task<List<InsurancePlanDto>> GetAllPlansAsync()
        {
            var plans = await _insurancePlanRepository.GetAllAsync();
            return _mapper.Map<List<InsurancePlanDto>>(plans);
        }

        public async Task<List<InsurancePlanDto>> GetActivePlansAsync()
        {
            // Flaw 11 Fix: Filter at DB level to avoid large memory footprint
            var activePlans = await _insurancePlanRepository.GetActivePlansAsync();
            return _mapper.Map<List<InsurancePlanDto>>(activePlans);
        }

        public async Task<InsurancePlanDto?> GetPlanByIdAsync(Guid id)
        {
            var plan = await _insurancePlanRepository.GetByIdAsync(id);
            return _mapper.Map<InsurancePlanDto>(plan);
        }

        public async Task<InsurancePlanDto> CreatePlanAsync(CreateInsurancePlanDto dto)
        {
            // Check if plan code exists
            var existing = await _insurancePlanRepository.GetByPlanCodeAsync(dto.PlanCode);
            if (existing != null)
            {
                throw new ConflictException($"Insurance Plan with code {dto.PlanCode} already exists.");
            }

            var planCoverages = dto.Coverages.Select(c => new PlanCoverage
            {
                Type = Enum.Parse<CoverageType>(c.Type, true),
                CoverageAmount = c.CoverageAmount,
                CoveredGroup = Enum.Parse<CoveredGroup>(c.CoveredGroup, true),
                IsActive = true
            }).ToList();

            var plan = new InsurancePlan
            {
                PlanCode = dto.PlanCode,
                PlanName = dto.PlanName,
                BasePremium = dto.BasePremium,
                Description = dto.Description,
                Status = true,
                CreatedAt = DateTime.UtcNow,
                Coverages = planCoverages
            };

            await _insurancePlanRepository.AddAsync(plan);
            await _unitOfWork.SaveChangesAsync();

            // Notify Admins
            try
            {
                var admins = await _userRepo.GetAllByRoleAsync(UserRole.Admin);
                foreach (var admin in admins)
                {
                    await _notificationService.CreateNotificationAsync(admin.Id, "Product Launch", $"New Insurance Plan '{plan.PlanName}' ({plan.PlanCode}) has been created.", "Success");
                }
            }
            catch { }

            return _mapper.Map<InsurancePlanDto>(plan);
        }

        public async Task<InsurancePlanDto> UpdatePlanAsync(Guid id, UpdateInsurancePlanDto dto)
        {
            var plan = await _insurancePlanRepository.GetByIdAsync(id);
            if (plan == null)
            {
                throw new NotFoundException("Insurance Plan not found.");
            }

            plan.PlanName = dto.PlanName;
            plan.BasePremium = dto.BasePremium;
            plan.Description = dto.Description;
            plan.Status = dto.Status;

            // Simple update strategy: Create new, delete old
            plan.Coverages.Clear();
            foreach (var c in dto.Coverages)
            {
                plan.Coverages.Add(new PlanCoverage
                {
                    InsurancePlanId = plan.Id,
                    Type = Enum.Parse<CoverageType>(c.Type, true),
                    CoverageAmount = c.CoverageAmount,
                    CoveredGroup = Enum.Parse<CoveredGroup>(c.CoveredGroup, true),
                    IsActive = true
                });
            }

            _insurancePlanRepository.Update(plan);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<InsurancePlanDto>(plan);
        }

        public async Task<bool> DeactivatePlanAsync(Guid id)
        {
            var plan = await _insurancePlanRepository.GetByIdAsync(id);
            if (plan == null) return false;

            plan.Status = false;
            _insurancePlanRepository.Update(plan);
            await _unitOfWork.SaveChangesAsync();

            // Notify Admins
            try
            {
                var admins = await _userRepo.GetAllByRoleAsync(UserRole.Admin);
                foreach (var admin in admins)
                {
                    await _notificationService.CreateNotificationAsync(admin.Id, "Product Deactivated", $"Insurance Plan '{plan.PlanName}' has been deactivated.", "Warning");
                }
            }
            catch { }

            return true;
        }

        public async Task<bool> DeletePlanAsync(Guid id)
        {
            var plan = await _insurancePlanRepository.GetByIdAsync(id);
            if (plan == null) return false;

            // Flaw 10 Fix: Prevent deleting plans that are currently in use
            bool inUse = await _insurancePlanRepository.IsPlanInUseAsync(id);
            if (inUse)
            {
                throw new BadRequestException("This plan cannot be deleted because it is currently assigned to active company policies. Deactivate it instead.");
            }

            _insurancePlanRepository.Delete(plan);
            
            // Flaw 16 Fix: Audit Plan Deletion
            await _auditRepo.AddAsync(new AuditLog
            {
                Id = Guid.NewGuid(),
                Action = "DeletePlan",
                EntityName = "InsurancePlan",
                EntityId = id.ToString(),
                NewValues = $"Plan {plan.PlanName} ({plan.PlanCode}) was permanently deleted.",
                Timestamp = DateTime.UtcNow
            });

            await _unitOfWork.SaveChangesAsync();

            // Notify Admins
            try
            {
                var admins = await _userRepo.GetAllByRoleAsync(UserRole.Admin);
                foreach (var admin in admins)
                {
                    await _notificationService.CreateNotificationAsync(admin.Id, "Product Deleted", $"Insurance Plan '{plan.PlanName}' has been permanently deleted.", "Error");
                }
            }
            catch { }

            return true;
        }
    }
}
