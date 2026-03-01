using System.Threading.Tasks;
using EGI_Backend.Application.DTOs;

namespace EGI_Backend.Application.Interfaces
{
    public interface IPolicyAssignmentService
    {
        Task<string> ProcessMembersExcelAsync(UploadMembersDto dto);
    }
}
