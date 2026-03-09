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

        public InsurancePlanService(IUnitOfWork unitOfWork, IInsurancePlanRepository insurancePlanRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _insurancePlanRepository = insurancePlanRepository;
            _mapper = mapper;
        }

        public async Task<List<InsurancePlanDto>> GetAllPlansAsync()
        {
            var plans = await _insurancePlanRepository.GetAllAsync();
            return _mapper.Map<List<InsurancePlanDto>>(plans);
        }

        public async Task<List<InsurancePlanDto>> GetActivePlansAsync()
        {
            var plans = await _insurancePlanRepository.GetAllAsync();
            var activePlans = plans.Where(p => p.Status).ToList();
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

            return true;
        }

        public async Task<bool> DeletePlanAsync(Guid id)
        {
            var plan = await _insurancePlanRepository.GetByIdAsync(id);
            if (plan == null) return false;

            _insurancePlanRepository.Delete(plan);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
