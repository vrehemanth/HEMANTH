using System.Threading.Tasks;

namespace EGI_Backend.Application.Interfaces
{
    public interface IDataService
    {
        Task<object> GetUserClaims(string userId, string role);
        Task<object> GetPolicies(string userId, string role);
        Task<object> GetMembers(string userId, string role);
        Task<object> GetInvoices(string userId, string role);
        Task<object> GetEndorsements(string userId, string role);
        Task<object> GetSummary(string userId, string role);
        Task<object> GetAvailablePlans();
        Task<object> GetHospitals();
        Task<object> GetProfile(string userId, string role);
        Task<object> GetCommissionLogs(string userId, string role);
        Task<object> GetStaff(string userId, string role);
        Task<object> GetAuditLogs(string userId, string role);
        Task<object> GetPendingClients(string userId, string role);
    }
}
