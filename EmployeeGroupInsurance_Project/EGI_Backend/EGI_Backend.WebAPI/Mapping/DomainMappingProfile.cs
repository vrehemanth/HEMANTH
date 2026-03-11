using AutoMapper;
using EGI_Backend.Application.DTOs;
using EGI_Backend.Domain.Entities;
using EGI_Backend.Domain.Enums;

namespace EGI_Backend.WebAPI.Mapping
{
    public class DomainMappingProfile : Profile
    {
        public DomainMappingProfile()
        {
            // Insurance Plan mappings
            CreateMap<InsurancePlan, InsurancePlanDto>();
            CreateMap<PlanCoverage, PlanCoverageDto>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
                .ForMember(dest => dest.CoveredGroup, opt => opt.MapFrom(src => src.CoveredGroup.ToString()));

            // Claim mappings
            CreateMap<Claim, ClaimResponseDto>()
                .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Member.FullName))
                .ForMember(dest => dest.DependentName, opt => opt.MapFrom(src => src.Dependent != null ? src.Dependent.FullName : null))
                .ForMember(dest => dest.ClaimType, opt => opt.MapFrom(src => src.ClaimType.ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.ReviewedByName, opt => opt.MapFrom(src => src.ReviewedByUser != null && src.Status != ClaimStatus.InReview ? src.ReviewedByUser.Name : null))
                .ForMember(dest => dest.InReviewByOfficerName, opt => opt.MapFrom(src => src.ReviewedByUser != null && src.Status == ClaimStatus.InReview ? src.ReviewedByUser.Name : null));

            CreateMap<Claim, ClaimDetailResponseDto>()
                .ForMember(dest => dest.PolicyNo, opt => opt.MapFrom(src => src.PolicyAssignment != null ? src.PolicyAssignment.PolicyNo : string.Empty))
                .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Member.FullName))
                .ForMember(dest => dest.EmployeeCode, opt => opt.MapFrom(src => src.Member.EmployeeCode))
                .ForMember(dest => dest.DependentName, opt => opt.MapFrom(src => src.Dependent != null ? src.Dependent.FullName : null))
                .ForMember(dest => dest.DependentRelationship, opt => opt.MapFrom(src => src.Dependent != null ? src.Dependent.Relationship.ToString() : null))
                .ForMember(dest => dest.ClaimType, opt => opt.MapFrom(src => src.ClaimType.ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.ReviewedByName, opt => opt.MapFrom(src => src.ReviewedByUser != null ? src.ReviewedByUser.Name : null));

            CreateMap<ClaimDocument, ClaimDocumentDto>()
                .ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => src.DocumentType.ToString()))
                .ForMember(dest => dest.FileUrl, opt => opt.MapFrom(src => "/uploads/" + System.IO.Path.GetFileName(src.FilePath)));

            // Invoice mappings
            CreateMap<Invoice, InvoiceResponseDto>()
                .ForMember(dest => dest.PolicyNo, opt => opt.MapFrom(src => src.PolicyAssignment != null ? src.PolicyAssignment.PolicyNo : string.Empty))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.IsPaymentBlocked, opt => opt.MapFrom(src => !src.InvoiceNo.EndsWith("-ADJ") && (DateTime.UtcNow.Date - src.DueDate.Date).Days >= 7));

            CreateMap<Payment, PaymentResponseDto>()
                .ForMember(dest => dest.InvoiceNo, opt => opt.MapFrom(src => src.Invoice != null ? src.Invoice.InvoiceNo : string.Empty))
                .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethod.ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            // Policy Assignment mappings
            CreateMap<PolicyAssignment, PolicyAssignmentResponseDto>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CorporateClient.CompanyName))
                .ForMember(dest => dest.PlanName, opt => opt.MapFrom(src => src.InsurancePlan.PlanName))
                .ForMember(dest => dest.TotalPremium, opt => opt.MapFrom(src => src.TotalPremium))
                .ForMember(dest => dest.BillingFrequency, opt => opt.MapFrom(src => src.BillingFrequency.ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            // Audit Log mappings
            CreateMap<AuditLog, AuditLogResponseDto>();

            // User mappings
            CreateMap<User, UserResponseDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<Member, MemberResponseDto>()
                .ForMember(dest => dest.PolicyNo, opt => opt.MapFrom(src => src.PolicyAssignment != null ? src.PolicyAssignment.PolicyNo : null));

            CreateMap<Dependent, DependentResponseDto>()
                .ForMember(dest => dest.Relationship, opt => opt.MapFrom(src => src.Relationship.ToString()))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()));

            CreateMap<PolicyEndorsement, EndorsementResponseDto>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}
