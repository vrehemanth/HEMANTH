using AutoMapper;
using EGI_Backend.Application.DTOs;
using EGI_Backend.Application.Interfaces;
using EGI_Backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EGI_Backend.Application.Services
{
    public class HospitalService : IHospitalService
    {
        private readonly IHospitalRepository _hospitalRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HospitalService(IHospitalRepository hospitalRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _hospitalRepository = hospitalRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<HospitalDto>> GetAllHospitalsAsync()
        {
            var hospitals = await _hospitalRepository.GetAllAsync();
            return _mapper.Map<List<HospitalDto>>(hospitals);
        }

        public async Task<HospitalDto?> GetHospitalByIdAsync(Guid id)
        {
            var hospital = await _hospitalRepository.GetByIdAsync(id);
            return _mapper.Map<HospitalDto>(hospital);
        }

        public async Task<List<HospitalDto>> GetNetworkHospitalsAsync()
        {
            var hospitals = await _hospitalRepository.GetNetworkHospitalsAsync();
            return _mapper.Map<List<HospitalDto>>(hospitals);
        }

        public async Task<HospitalDto> CreateHospitalAsync(CreateUpdateHospitalDto dto)
        {
            var hospital = _mapper.Map<Hospital>(dto);
            hospital.CreatedAt = DateTime.UtcNow;

            await _hospitalRepository.AddAsync(hospital);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<HospitalDto>(hospital);
        }

        public async Task<HospitalDto?> UpdateHospitalAsync(Guid id, CreateUpdateHospitalDto dto)
        {
            var hospital = await _hospitalRepository.GetByIdAsync(id);
            if (hospital == null) return null;

            _mapper.Map(dto, hospital);
            hospital.UpdatedAt = DateTime.UtcNow;

            await _hospitalRepository.UpdateAsync(hospital);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<HospitalDto>(hospital);
        }

        public async Task<bool> DeleteHospitalAsync(Guid id)
        {
            var exists = await _hospitalRepository.ExistsAsync(id);
            if (!exists) return false;

            await _hospitalRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
