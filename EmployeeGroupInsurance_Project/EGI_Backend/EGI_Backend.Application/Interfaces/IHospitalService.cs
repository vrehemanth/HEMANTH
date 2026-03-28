using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EGI_Backend.Application.DTOs;

namespace EGI_Backend.Application.Interfaces
{
    public interface IHospitalService
    {
        Task<List<HospitalDto>> GetAllHospitalsAsync();
        Task<HospitalDto?> GetHospitalByIdAsync(Guid id);
        Task<List<HospitalDto>> GetNetworkHospitalsAsync();
        Task<HospitalDto> CreateHospitalAsync(CreateUpdateHospitalDto dto);
        Task<HospitalDto?> UpdateHospitalAsync(Guid id, CreateUpdateHospitalDto dto);
        Task<bool> DeleteHospitalAsync(Guid id);
    }
}
