using System;
using System.Threading.Tasks;
using EGI_Backend.Application.Interfaces;

namespace EGI_Backend.Application.Services
{
    public class DataService : IDataService
    {
        private readonly ICustomerDashboardService _customer;
        private readonly IAdminDashboardService _admin;
        private readonly IAgentDashboardService _agent;
        private readonly IClaimsOfficerDashboardService _officer;
        private readonly IInsurancePlanService _plans;
        private readonly IInvoiceRepository _invoiceRepo;
        private readonly IPolicyEndorsementRepository _endorsementRepo;
        private readonly IHospitalService _hospitalService;

        public DataService(
            ICustomerDashboardService customer,
            IAdminDashboardService admin,
            IAgentDashboardService agent,
            IClaimsOfficerDashboardService officer,
            IInsurancePlanService plans,
            IInvoiceRepository invoiceRepo,
            IPolicyEndorsementRepository endorsementRepo,
            IHospitalService hospitalService)
        {
            _customer = customer;
            _admin = admin;
            _agent = agent;
            _officer = officer;
            _plans = plans;
            _invoiceRepo = invoiceRepo;
            _endorsementRepo = endorsementRepo;
            _hospitalService = hospitalService;
        }

        public async Task<object> GetSummary(string userId, string role)
        {
            if (role == "Admin") return await _admin.GetSummaryAsync();
            if (role == "ClaimsOfficer" && Guid.TryParse(userId, out var oid)) return await _officer.GetSummaryAsync(oid);
            if (role == "Agent" && Guid.TryParse(userId, out var aid)) return await _agent.GetSummaryAsync(aid);
            if (role == "Customer" && Guid.TryParse(userId, out var cid)) return await _customer.GetSummaryAsync(cid);
            return "Summary unavailable.";
        }

        public async Task<object> GetUserClaims(string userId, string role)
        {
            if (role == "Admin" || role == "ClaimsOfficer")
                return await _admin.GetAllClaimsAsync();
            if (role == "Customer" && Guid.TryParse(userId, out var cid))
                return await _customer.GetMyClaimsAsync(cid);
            return "No claims data available.";
        }

        public async Task<object> GetPolicies(string userId, string role)
        {
            if (role == "Admin" || role == "ClaimsOfficer")
                return await _admin.GetAllPolicyAssignmentsAsync();
            if (role == "Customer" && Guid.TryParse(userId, out var cid))
                return await _customer.GetMyPoliciesAsync(cid);
            if (role == "Agent" && Guid.TryParse(userId, out var aid))
                return await _agent.GetMyPoliciesAsync(aid);
            return "No policy data available.";
        }

        public async Task<object> GetMembers(string userId, string role)
        {
            if (role == "Customer" && Guid.TryParse(userId, out var cid))
                return await _customer.GetMyMembersAsync(cid);
            if (role == "Admin" || role == "ClaimsOfficer")
                return await _admin.GetAllClientsAsync();
            if (role == "Agent" && Guid.TryParse(userId, out var aid))
                return await _agent.GetMyCustomersAsync(aid);
            return "No member data available.";
        }

        public async Task<object> GetInvoices(string userId, string role)
        {
            if (role == "Admin" || role == "ClaimsOfficer")
                return await _invoiceRepo.GetAllAsync();
            if (role == "Customer" && Guid.TryParse(userId, out var cid))
                return await _customer.GetMyInvoicesAsync(cid);
            return "No invoice data available.";
        }

        public async Task<object> GetEndorsements(string userId, string role)
        {
            if (role == "Admin" || role == "ClaimsOfficer")
                return await _endorsementRepo.GetAllAsync();
            if (role == "Customer" && Guid.TryParse(userId, out var cid))
                return await _customer.GetMyEndorsementsAsync(cid);
            return "No endorsement data available.";
        }

        public async Task<object> GetAvailablePlans()
        {
            return await _plans.GetAllPlansAsync();
        }

        public async Task<object> GetHospitals()
        {
            return await _hospitalService.GetNetworkHospitalsAsync();
        }

        public async Task<object> GetProfile(string userId, string role)
        {
            if (role == "Customer" && Guid.TryParse(userId, out var cid))
                return await _customer.GetProfileAsync(cid);
            return "Profile data not available for this role.";
        }

        // === Admin-specific endpoints ===
        public async Task<object> GetStaff(string userId, string role)
        {
            if (role == "Admin")
            {
                var agents = await _admin.GetAllStaffAsync("Agent");
                var officers = await _admin.GetAllStaffAsync("ClaimsOfficer");
                return new { Agents = agents, ClaimsOfficers = officers };
            }
            return "Staff data requires Admin access.";
        }

        public async Task<object> GetAuditLogs(string userId, string role)
        {
            if (role == "Admin")
                return await _admin.GetAuditLogsAsync();
            return "Audit logs require Admin access.";
        }

        public async Task<object> GetPendingClients(string userId, string role)
        {
            if (role == "Admin")
                return await _admin.GetPendingClientsAsync();
            return "Pending clients data requires Admin access.";
        }

        // === Agent-specific endpoints ===
        public async Task<object> GetCommissionLogs(string userId, string role)
        {
            if (role == "Agent" && Guid.TryParse(userId, out var aid))
                return await _agent.GetCommissionLogsAsync(aid);
            return "Commission data requires Agent access.";
        }
    }
}
