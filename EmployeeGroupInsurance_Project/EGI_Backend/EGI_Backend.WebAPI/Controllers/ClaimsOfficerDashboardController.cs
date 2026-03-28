using EGI_Backend.Application.DTOs;
using EGI_Backend.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EGI_Backend.WebAPI.Controllers
{
    [Authorize(Roles = "ClaimsOfficer,Admin")]
    [Route("api/claims-officer/dashboard")]
    public class ClaimsOfficerDashboardController : BaseApiController
    {
        private readonly IClaimsOfficerDashboardService _dashboardService;
        private readonly IClaimService _claimService;
        private readonly IInsurancePlanService _planService;
        private readonly IInvoiceService _invoiceService;

        public ClaimsOfficerDashboardController(
            IClaimsOfficerDashboardService dashboardService,
            IClaimService claimService,
            IInsurancePlanService planService,
            IInvoiceService invoiceService)
        {
            _dashboardService = dashboardService;
            _claimService = claimService;
            _planService = planService;
            _invoiceService = invoiceService;
        }

        [HttpGet("invoices/policy/{policyAssignmentId}")]
        public async Task<IActionResult> GetInvoicesByPolicy(Guid policyAssignmentId)
        {
            var role = CurrentUserRole ?? "ClaimsOfficer";
            var invoices = await _invoiceService.GetInvoicesByPolicyAsync(policyAssignmentId, CurrentUserId, role);
            return Ok(invoices);
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

        [HttpGet("claims/policy/{policyAssignmentId}")]
        public async Task<IActionResult> GetClaimsByPolicy(Guid policyAssignmentId)
        {
            var claims = await _claimService.GetClaimsByPolicyAsync(policyAssignmentId, CurrentUserId, CurrentUserRole ?? "ClaimsOfficer");
            return Ok(claims);
        }

        [HttpGet("claims/member/{memberId}/history")]
        public async Task<IActionResult> GetClaimsByMember(Guid memberId)
        {
            var claims = await _claimService.GetClaimsByMemberAsync(memberId, CurrentUserId, CurrentUserRole ?? "ClaimsOfficer");
            return Ok(claims);
        }

        [HttpGet("claims/{claimId}/detail")]
        public async Task<IActionResult> GetClaimDetail(Guid claimId)
        {
            var role = User.FindFirstValue(ClaimTypes.Role) ?? "ClaimsOfficer";
            var claim = await _claimService.GetClaimDetailAsync(claimId, CurrentUserId, role);
            return Ok(claim);
        }

        [HttpGet("coverage-summary/member/{memberId}")]
        public async Task<IActionResult> GetMemberCoverageSummary(Guid memberId)
        {
            var summary = await _claimService.GetCoverageSummaryAsync(memberId, null);
            return Ok(summary);
        }

        [HttpGet("coverage-summary/member/{memberId}/dependent/{dependentId}")]
        public async Task<IActionResult> GetDependentCoverageSummary(Guid memberId, Guid dependentId)
        {
            var summary = await _claimService.GetCoverageSummaryAsync(memberId, dependentId);
            return Ok(summary);
        }

        [HttpGet("summary")]
        public async Task<ActionResult<ClaimsOfficerDashboardSummaryDto>> GetSummary()
        {
            var summary = await _dashboardService.GetSummaryAsync(CurrentUserId);
            return Ok(summary);
        }

        [HttpGet("pending-claims")]
        public async Task<ActionResult<List<ClaimResponseDto>>> GetPendingClaims()
        {
            var claims = await _claimService.GetPendingClaimsAsync();
            return Ok(claims);
        }

        [HttpPost("review/{claimId}")]
        public async Task<IActionResult> ReviewClaim(Guid claimId, [FromBody] ReviewClaimDto dto)
        {
            await _claimService.ReviewClaimAsync(CurrentUserId, claimId, dto);
            return Ok(new { message = dto.IsApproved ? "Claim approved successfully." : "Claim rejected successfully." });
        }

        [HttpGet("history")]
        public async Task<ActionResult<List<ClaimResponseDto>>> GetHistory()
        {
            var history = await _claimService.GetClaimsReviewedByOfficerAsync(CurrentUserId);
            return Ok(history);
        }

        [HttpPost("claims/{id}/take")]
        public async Task<IActionResult> TakeClaim(Guid id)
        {
            await _claimService.TakeClaimAsync(CurrentUserId, id);
            return Ok(new { message = "Claim locked for review." });
        }

        [HttpPost("claims/{id}/release")]
        public async Task<IActionResult> ReleaseClaim(Guid id)
        {
            await _claimService.ReleaseClaimAsync(id);
            return Ok(new { message = "Claim released back to queue." });
        }

        [HttpPost("claims/{id}/run-ai")]
        public async Task<IActionResult> RunAI(Guid id)
        {
            var result = await _claimService.RunAIAdjudicationAsync(id);
            return Ok(result);
        }

        [HttpPost("claims/partnership")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> RegisterPartnershipClaim([FromForm] SubmitClaimDto dto)
        {
            var claimNumber = await _claimService.RegisterPartnershipClaimAsync(CurrentUserId, dto);
            return Ok(new { message = "Partnership bill registered successfully and sent for Admin approval.", claimNumber });
        }

        [HttpGet("live-dispatches")]
        public async Task<IActionResult> GetLiveDispatches()
        {
            var dispatches = await _claimService.GetLiveDispatchesAsync();
            return Ok(dispatches);
        }

        [HttpGet("members/search/{identifier}")]
        public async Task<ActionResult<MemberSearchResultDto>> SearchMember(string identifier)
        {
            var member = await _claimService.SearchMemberAsync(identifier);
            return Ok(member);
        }

        [HttpGet("pending-health-checkups")]
        public async Task<IActionResult> GetPendingHealthCheckups()
        {
            var result = await _dashboardService.GetPendingHealthCheckupsAsync();
            return Ok(result);
        }

        [HttpPost("update-health-checkup-actuals/{corporateClientId}")]
        public async Task<IActionResult> UpdateHealthCheckupActuals(Guid corporateClientId, [FromBody] HealthCheckActualsDto dto)
        {
            var result = await _dashboardService.UpdateHealthCheckupActualsAsync(corporateClientId, dto.MemberCount, dto.DependentCount);
            if (!result) return NotFound("Corporate Client not found.");
            return Ok(new { message = "Attendance counts synchronized successfully." });
        }
    }

    public class HealthCheckActualsDto
    {
        public int MemberCount { get; set; }
        public int DependentCount { get; set; }
    }
}
