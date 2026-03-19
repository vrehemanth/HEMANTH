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
    }
}
