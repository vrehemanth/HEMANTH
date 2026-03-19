using AutoMapper;
using EGI_Backend.Application.Contracts.Auth;
using EGI_Backend.Application.DTOs;
using EGI_Backend.Domain.Entities;
using EGI_Backend.Domain.Enums;

namespace EGI_Backend.WebAPI.Mapping
{
    public class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
            CreateMap<RegisterRequest, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.MapFrom(_ => UserRole.Customer))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => UserStatus.Active))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<AdminRegisterRequest, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.MapFrom(_ => UserRole.Admin))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => UserStatus.Active))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<AdminCreateStaffRequest, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => UserStatus.Active))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<CorporateClient, CorporateClientResponseDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.IsBlocked, opt => opt.MapFrom(src => src.IsBlocked))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User != null ? src.User.Email : string.Empty))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone ?? string.Empty))
                .ForMember(dest => dest.IndustryType, opt => opt.MapFrom(src => (int)src.IndustryType))
                .ForMember(dest => dest.KybAiAnalysis, opt => opt.MapFrom(src => src.KybAiAnalysis))
                .ForMember(dest => dest.KybAiConfidenceScore, opt => opt.MapFrom(src => src.KybAiConfidenceScore));

            CreateMap<CorporateClientDocument, CorporateClientDocumentDto>()
                .ForMember(dest => dest.DocumentType, 
                    opt => opt.MapFrom(src => src.DocumentType.ToString()))
                .ForMember(dest => dest.FileUrl, 
                    opt => opt.MapFrom(src => "/uploads/" + System.IO.Path.GetFileName(src.FilePath)));
        }
    }
}
