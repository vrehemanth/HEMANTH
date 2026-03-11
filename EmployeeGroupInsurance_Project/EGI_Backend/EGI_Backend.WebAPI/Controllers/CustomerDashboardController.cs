using EGI_Backend.Application.DTOs;
using EGI_Backend.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EGI_Backend.WebAPI.Controllers
{
    [Authorize(Roles = "Customer")]
    [Authorize(Roles = "Customer")]
    [Route("api/customer/dashboard")]
    public class CustomerDashboardController : BaseApiController
    {
        private readonly ICustomerDashboardService _dashboardService;
        private readonly ICorporateClientService _clientService;
        private readonly IPolicyAssignmentService _policyService;
        private readonly IInvoiceService _invoiceService;
        private readonly IClaimService _claimService;
        private readonly IPolicyEndorsementService _endorsementService;
        private readonly IInsurancePlanService _planService;

        public CustomerDashboardController(
            ICustomerDashboardService dashboardService,
            ICorporateClientService clientService,
            IPolicyAssignmentService policyService,
            IInvoiceService invoiceService,
            IClaimService claimService,
            IPolicyEndorsementService endorsementService,
            IInsurancePlanService planService)
        {
            _dashboardService    = dashboardService;
            _clientService       = clientService;
            _policyService       = policyService;
            _invoiceService      = invoiceService;
            _claimService        = claimService;
            _endorsementService  = endorsementService;
            _planService         = planService;
        }



        // ─── Profile ──────────────────────────────────────────────

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var result = await _dashboardService.GetProfileAsync(CurrentUserId);
            return Ok(result);
        }

        // ─── Insurance Plans ─────────────────────────────────────

        [HttpGet("insurance-plans")]
        public async Task<ActionResult<List<InsurancePlanDto>>> GetAllPlans()
        {
            var plans = await _planService.GetActivePlansAsync();
            return Ok(plans);
        }

        [HttpGet("insurance-plans/{id}")]
        public async Task<IActionResult> GetInsurancePlanById(Guid id)
        {
            var plan = await _planService.GetPlanByIdAsync(id);
            if (plan == null) return NotFound("Plan not found");
            return Ok(plan);
        }

        // ─── Endorsements ────────────────────────────────────────

        [HttpPost("submit-endorsement")]
        public async Task<IActionResult> SubmitEndorsement([FromBody] SubmitEndorsementDto dto)
        {
            var result = await _endorsementService.SubmitEndorsementAsync(CurrentUserId, dto);
            return Ok(result);
        }

        [HttpGet("endorsements/policy/{policyId}")]
        public async Task<IActionResult> GetEndorsementsByPolicy(Guid policyId)
        {
            var result = await _endorsementService.GetEndorsementsByPolicyAsync(policyId);
            return Ok(result);
        }

        // ─── Profile & Documents ─────────────────────────────────

        [HttpPost("complete-profile")]
        public async Task<IActionResult> CompleteProfile(CreateCorporateProfileDto dto)
        {
            await _clientService.CreateProfileAsync(CurrentUserId, dto);
            return Ok(new { message = "Profile created successfully." });
        }

        [HttpPost("upload-document")]
        public async Task<IActionResult> UploadDocument([FromForm] UploadCorporateDocumentDto dto)
        {
            var result = await _clientService.UploadDocumentAsync(CurrentUserId, dto);
            return Ok(result);
        }

        // ─── Members ─────────────────────────────────────────────

        [HttpPost("upload-members")]
        public async Task<IActionResult> UploadMembersExcel([FromForm] UploadMembersDto dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                return BadRequest(new { message = "Validation Failed: " + errors });
            }

            var resultMessage = await _policyService.ProcessMembersExcelAsync(dto);
            return Ok(new { message = resultMessage });
        }

        // ─── Invoices ────────────────────────────────────────────

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

        [HttpPost("pay-invoice/{invoiceId}")]
        public async Task<IActionResult> PayInvoice(Guid invoiceId, [FromBody] PayInvoiceDto dto)
        {
            var result = await _invoiceService.PayInvoiceAsync(invoiceId, CurrentUserId, dto);
            return Ok(result);
        }

        [HttpGet("invoices/policy/{policyAssignmentId}")]
        public async Task<IActionResult> GetInvoicesByPolicy(Guid policyAssignmentId)
        {
            var invoices = await _invoiceService.GetInvoicesByPolicyAsync(policyAssignmentId);
            return Ok(invoices);
        }

        // ─── Claims ──────────────────────────────────────────────

        [HttpPost("submit-claim")]
        public async Task<IActionResult> SubmitClaim([FromForm] SubmitClaimDto dto)
        {
            var result = await _claimService.SubmitClaimAsync(CurrentUserId, dto);
            return Ok(new { message = result });
        }

        [HttpGet("claims/policy/{policyAssignmentId}")]
        public async Task<IActionResult> GetClaimsByPolicy(Guid policyAssignmentId)
        {
            var claims = await _claimService.GetClaimsByPolicyAsync(policyAssignmentId);
            return Ok(claims);
        }

        // ─── Dashboard Aggregates ─────────────────────────────────

        [HttpGet("overview")]
        public async Task<IActionResult> GetOverview()
        {
            var overview = await _dashboardService.GetOverviewAsync(CurrentUserId);
            return Ok(overview);
        }

        [HttpGet("summary")]
        public async Task<ActionResult<CustomerDashboardSummaryDto>> GetSummary()
        {
            var summary = await _dashboardService.GetSummaryAsync(CurrentUserId);
            return Ok(summary);
        }

        [HttpGet("my-policies")]
        public async Task<ActionResult<List<PolicyAssignmentResponseDto>>> GetMyPolicies()
        {
            var policies = await _dashboardService.GetMyPoliciesAsync(CurrentUserId);
            return Ok(policies);
        }

        [HttpGet("my-members")]
        public async Task<ActionResult<List<MemberResponseDto>>> GetMyMembers()
        {
            var members = await _dashboardService.GetMyMembersAsync(CurrentUserId);
            return Ok(members);
        }

        [HttpGet("my-claims")]
        public async Task<ActionResult<List<ClaimResponseDto>>> GetMyClaims()
        {
            var claims = await _dashboardService.GetMyClaimsAsync(CurrentUserId);
            return Ok(claims);
        }

        [HttpGet("my-invoices")]
        public async Task<ActionResult<List<InvoiceResponseDto>>> GetMyInvoices()
        {
            var invoices = await _dashboardService.GetMyInvoicesAsync(CurrentUserId);
            return Ok(invoices);
        }

        [HttpGet("my-endorsements")]
        public async Task<ActionResult<List<EndorsementResponseDto>>> GetMyEndorsements()
        {
            var endorsements = await _dashboardService.GetMyEndorsementsAsync(CurrentUserId);
            return Ok(endorsements);
        }
    }
}
