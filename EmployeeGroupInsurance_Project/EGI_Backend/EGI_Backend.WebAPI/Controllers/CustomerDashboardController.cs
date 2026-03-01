using EGI_Backend.Application.DTOs;
using EGI_Backend.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EGI_Backend.WebAPI.Controllers
{
    [Authorize(Roles = "Customer")]
    [ApiController]
    [Route("api/customer/dashboard")]
    public class CustomerDashboardController : ControllerBase
    {
        private readonly ICustomerDashboardService _dashboardService;
        private readonly ICorporateClientService _clientService;
        private readonly IPolicyAssignmentService _policyService;
        private readonly IInvoiceService _invoiceService;
        private readonly IClaimService _claimService;
        private readonly IPolicyEndorsementService _endorsementService;

        public CustomerDashboardController(
            ICustomerDashboardService dashboardService,
            ICorporateClientService clientService,
            IPolicyAssignmentService policyService,
            IInvoiceService invoiceService,
            IClaimService claimService,
            IPolicyEndorsementService endorsementService)
        {
            _dashboardService = dashboardService;
            _clientService = clientService;
            _policyService = policyService;
            _invoiceService = invoiceService;
            _claimService = claimService;
            _endorsementService = endorsementService;
        }

        [HttpPost("submit-endorsement")]
        public async Task<IActionResult> SubmitEndorsement([FromBody] SubmitEndorsementDto dto)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _endorsementService.SubmitEndorsementAsync(userId, dto);
            return Ok(result);
        }

        [HttpGet("endorsements/policy/{policyId}")]
        public async Task<IActionResult> GetEndorsementsByPolicy(Guid policyId)
        {
            var result = await _endorsementService.GetEndorsementsByPolicyAsync(policyId);
            return Ok(result);
        }

        [HttpPost("complete-profile")]
        public async Task<IActionResult> CompleteProfile(CreateCorporateProfileDto dto)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            await _clientService.CreateProfileAsync(userId, dto);
            return Ok(new { message = "Profile created successfully." });
        }

        [HttpPost("upload-document")]
        public async Task<IActionResult> UploadDocument([FromForm] UploadCorporateDocumentDto dto)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _clientService.UploadDocumentAsync(userId, dto);
            return Ok(result);
        }

        [HttpPost("upload-members")]
        public async Task<IActionResult> UploadMembersExcel([FromForm] UploadMembersDto dto)
        {
            try
            {
                var resultMessage = await _policyService.ProcessMembersExcelAsync(dto);
                return Ok(new { message = resultMessage });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
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

        [HttpPost("pay-invoice/{invoiceId}")]
        public async Task<IActionResult> PayInvoice(Guid invoiceId, [FromBody] PayInvoiceDto dto)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _invoiceService.PayInvoiceAsync(invoiceId, userId, dto);
            return Ok(result);
        }

        [HttpPost("submit-claim")]
        public async Task<IActionResult> SubmitClaim([FromForm] SubmitClaimDto dto)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _claimService.SubmitClaimAsync(userId, dto);
            return Ok(new { message = result });
        }

        [HttpGet("invoices/policy/{policyAssignmentId}")]
        public async Task<IActionResult> GetInvoicesByPolicy(Guid policyAssignmentId)
        {
            var invoices = await _invoiceService.GetInvoicesByPolicyAsync(policyAssignmentId);
            return Ok(invoices);
        }

        [HttpGet("claims/policy/{policyAssignmentId}")]
        public async Task<IActionResult> GetClaimsByPolicy(Guid policyAssignmentId)
        {
            var claims = await _claimService.GetClaimsByPolicyAsync(policyAssignmentId);
            return Ok(claims);
        }

        [HttpGet("summary")]
        public async Task<ActionResult<CustomerDashboardSummaryDto>> GetSummary()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
            {
                return Unauthorized();
            }

            var summary = await _dashboardService.GetSummaryAsync(userId);
            return Ok(summary);
        }

        [HttpGet("my-policies")]
        public async Task<ActionResult<List<PolicyAssignmentResponseDto>>> GetMyPolicies()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
            {
                return Unauthorized();
            }

            var policies = await _dashboardService.GetMyPoliciesAsync(userId);
            return Ok(policies);
        }

        [HttpGet("my-members")]
        public async Task<ActionResult<List<MemberResponseDto>>> GetMyMembers()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
            {
                return Unauthorized();
            }

            var members = await _dashboardService.GetMyMembersAsync(userId);
            return Ok(members);
        }

        [HttpGet("my-claims")]
        public async Task<ActionResult<List<ClaimResponseDto>>> GetMyClaims()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
            {
                return Unauthorized();
            }

            var claims = await _dashboardService.GetMyClaimsAsync(userId);
            return Ok(claims);
        }

        [HttpGet("my-invoices")]
        public async Task<ActionResult<List<InvoiceResponseDto>>> GetMyInvoices()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
            {
                return Unauthorized();
            }

            var invoices = await _dashboardService.GetMyInvoicesAsync(userId);
            return Ok(invoices);
        }
    }
}
