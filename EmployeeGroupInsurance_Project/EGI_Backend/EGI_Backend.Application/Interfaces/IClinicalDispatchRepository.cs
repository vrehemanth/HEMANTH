using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EGI_Backend.Domain.Entities;

namespace EGI_Backend.Application.Interfaces
{
    public interface IClinicalDispatchRepository
    {
        Task AddAsync(ClinicalDispatch dispatch);
        Task<List<ClinicalDispatch>> GetAllActiveAsync();
        Task<ClinicalDispatch?> GetByIdAsync(Guid id);
        Task UpdateAsync(ClinicalDispatch dispatch);
    }
}
