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
    [Authorize(Roles = "ClaimsOfficer")]
    [ApiController]
    [Route("api/claims-officer/dashboard")]
    public class ClaimsOfficerDashboardController : ControllerBase
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
            var invoices = await _invoiceService.GetInvoicesByPolicyAsync(policyAssignmentId);
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
            var claims = await _claimService.GetClaimsByPolicyAsync(policyAssignmentId);
            return Ok(claims);
        }

        [HttpGet("claims/member/{memberId}/history")]
        public async Task<IActionResult> GetClaimsByMember(Guid memberId)
        {
            var claims = await _claimService.GetClaimsByMemberAsync(memberId);
            return Ok(claims);
        }

        [HttpGet("claims/{claimId}/detail")]
        public async Task<IActionResult> GetClaimDetail(Guid claimId)
        {
            var claim = await _claimService.GetClaimDetailAsync(claimId);
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
            var officerIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(officerIdStr) || !Guid.TryParse(officerIdStr, out var officerId))
            {
                return Unauthorized();
            }

            var summary = await _dashboardService.GetSummaryAsync(officerId);
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
            var officerIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(officerIdStr) || !Guid.TryParse(officerIdStr, out var officerId))
            {
                return Unauthorized();
            }

            await _claimService.ReviewClaimAsync(officerId, claimId, dto);
            return Ok(new { message = dto.IsApproved ? "Claim approved successfully." : "Claim rejected successfully." });
        }
    }
}
