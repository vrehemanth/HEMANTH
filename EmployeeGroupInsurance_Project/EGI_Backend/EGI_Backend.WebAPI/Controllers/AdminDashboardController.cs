using AutoMapper;
using EGI_Backend.Application.DTOs;
using EGI_Backend.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EGI_Backend.WebAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/admin/dashboard")]
    public class AdminDashboardController : ControllerBase
    {
        private readonly IAdminDashboardService _dashboardService;
        private readonly IInsurancePlanService _planService;
        private readonly ICorporateClientService _corporateService;
        private readonly IClaimService _claimService;
        private readonly IInvoiceService _invoiceService;
        private readonly IPolicyEndorsementService _endorsementService;
        private readonly IMapper _mapper;

        public AdminDashboardController(
            IAdminDashboardService dashboardService, 
            IInsurancePlanService planService,
            ICorporateClientService corporateService,
            IClaimService claimService,
            IInvoiceService invoiceService,
            IPolicyEndorsementService endorsementService,
            IMapper mapper)
        {
            _dashboardService = dashboardService;
            _planService = planService;
            _corporateService = corporateService;
            _claimService = claimService;
            _invoiceService = invoiceService;
            _endorsementService = endorsementService;
            _mapper = mapper;
        }

        [HttpGet("claims/{claimId}/detail")]
        public async Task<IActionResult> GetClaimDetail(Guid claimId)
        {
            var claim = await _claimService.GetClaimDetailAsync(claimId);
            return Ok(claim);
        }

        [HttpGet("invoices/{invoiceId}")]
        public async Task<IActionResult> GetInvoiceDetail(Guid invoiceId)
        {
            var invoice = await _invoiceService.GetInvoiceByIdAsync(invoiceId);
            return Ok(invoice);
        }

        [HttpGet("invoices/{invoiceId}/payments")]
        public async Task<IActionResult> GetPayments(Guid invoiceId)
        {
            var payments = await _invoiceService.GetPaymentsByInvoiceAsync(invoiceId);
            return Ok(payments);
        }

        [HttpGet("invoices/policy/{policyAssignmentId}")]
        public async Task<IActionResult> GetInvoicesByPolicy(Guid policyAssignmentId)
        {
            var invoices = await _invoiceService.GetInvoicesByPolicyAsync(policyAssignmentId);
            return Ok(invoices);
        }

        [HttpGet("endorsements/pending")]
        public async Task<IActionResult> GetPendingEndorsements()
        {
            var result = await _endorsementService.GetPendingEndorsementsAsync();
            return Ok(result);
        }

        [HttpGet("endorsements/policy/{policyId}")]
        public async Task<IActionResult> GetEndorsementsByPolicy(Guid policyId)
        {
            var result = await _endorsementService.GetEndorsementsByPolicyAsync(policyId);
            return Ok(result);
        }

        [HttpGet("insurance-plans")]
        public async Task<ActionResult<List<InsurancePlanDto>>> GetAllPlans()
        {
            var plans = await _planService.GetAllPlansAsync();
            return Ok(plans);
        }

        [HttpGet("insurance-plans/{id}")]
        public async Task<ActionResult<InsurancePlanDto>> GetPlanById(Guid id)
        {
            var plan = await _planService.GetPlanByIdAsync(id);
            if (plan == null) return NotFound("Insurance Plan not found.");
            return Ok(plan);
        }

        [HttpPost("insurance-plans")]
        public async Task<ActionResult<InsurancePlanDto>> CreatePlan([FromBody] CreateInsurancePlanDto dto)
        {
            var plan = await _planService.CreatePlanAsync(dto);
            return CreatedAtAction(nameof(GetPlanById), new { id = plan.Id }, plan);
        }

        [HttpPut("insurance-plans/{id}")]
        public async Task<ActionResult<InsurancePlanDto>> UpdatePlan(Guid id, [FromBody] UpdateInsurancePlanDto dto)
        {
            var plan = await _planService.UpdatePlanAsync(id, dto);
            return Ok(plan);
        }

        [HttpDelete("insurance-plans/{id}/deactivate")]
        public async Task<ActionResult> DeactivatePlan(Guid id)
        {
            var result = await _planService.DeactivatePlanAsync(id);
            if (!result) return NotFound("Insurance Plan not found.");
            return Ok(new { message = "Insurance Plan deactivated successfully." });
        }

        [HttpDelete("insurance-plans/{id}")]
        public async Task<ActionResult> DeletePlan(Guid id)
        {
            var result = await _planService.DeletePlanAsync(id);
            if (!result) return NotFound("Insurance Plan not found.");
            return Ok(new { message = "Insurance Plan deleted successfully." });
        }

        [HttpGet("summary")]
        public async Task<ActionResult<DashboardSummaryDto>> GetSummary()
        {
            var summary = await _dashboardService.GetSummaryAsync();
            return Ok(summary);
        }

        [HttpGet("pending-clients")]
        public async Task<ActionResult<List<CorporateClientResponseDto>>> GetPendingClients()
        {
            var pending = await _dashboardService.GetPendingClientsAsync();
            return Ok(pending);
        }

        [HttpPost("approve-client/{id}")]
        public async Task<IActionResult> ApproveClient(Guid id, [FromBody] ReviewCorporateClientDto dto)
        {
            var adminIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(adminIdStr) || !Guid.TryParse(adminIdStr, out var adminId))
            {
                return Unauthorized();
            }

            var result = await _dashboardService.ReviewClientAsync(id, adminId, dto);
            if (!result) return NotFound("Client not found.");

            return Ok(new { message = dto.IsApproved ? "Client approved successfully." : "Client rejected successfully." });
        }

        [HttpGet("policy-assignments")]
        public async Task<ActionResult<List<PolicyAssignmentResponseDto>>> GetPolicyAssignments()
        {
            var assignments = await _dashboardService.GetAllPolicyAssignmentsAsync();
            return Ok(assignments);
        }

        [HttpGet("claims")]
        public async Task<ActionResult<List<ClaimResponseDto>>> GetClaims()
        {
            var claims = await _dashboardService.GetAllClaimsAsync();
            return Ok(claims);
        }

        [HttpGet("audit-logs")]
        public async Task<ActionResult<List<AuditLogResponseDto>>> GetAuditLogs([FromQuery] string? userId, [FromQuery] string? entityName)
        {
            var logs = await _dashboardService.GetAuditLogsAsync(userId, entityName);
            return Ok(logs);
        }

        [HttpGet("staff/{role}")]
        public async Task<ActionResult<List<UserResponseDto>>> GetStaff(string role)
        {
            var staff = await _dashboardService.GetAllStaffAsync(role);
            return Ok(staff); // roles: Agent, ClaimsOfficer
        }

        [HttpPost("toggle-user-status/{userId}")]
        public async Task<IActionResult> ToggleUserStatus(Guid userId)
        {
            var result = await _dashboardService.ToggleUserStatusAsync(userId);
            if (!result) return NotFound("User not found.");
            return Ok(new { message = "User status toggled successfully." });
        }

        [HttpGet("clients")]
        public async Task<ActionResult<List<CorporateClientResponseDto>>> GetAllClients()
        {
            var clients = await _dashboardService.GetAllClientsAsync();
            return Ok(clients);
        }

        [HttpPost("recalculate-commissions")]
        public async Task<IActionResult> RecalculateCommissions()
        {
            int updatedCount = await _dashboardService.RecalculateAllCommissionsAsync();
            return Ok(new { message = $"Recalculated commissions for {updatedCount} policies.", updatedCount });
        }
    }
}
