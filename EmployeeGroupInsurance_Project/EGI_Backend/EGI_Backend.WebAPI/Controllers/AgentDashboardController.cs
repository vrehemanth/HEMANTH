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
    [Authorize(Roles = "Agent")]
    [ApiController]
    [Route("api/agent/dashboard")]
    public class AgentDashboardController : ControllerBase
    {
        private readonly IAgentDashboardService _dashboardService;
        private readonly IAgentCustomerService _agentCustService;
        private readonly IPolicyAssignmentService _policyService;
        private readonly IInsurancePlanService _planService;
        private readonly IClaimService _claimService;
        private readonly IInvoiceService _invoiceService;
        private readonly IPolicyEndorsementService _endorsementService;

        public AgentDashboardController(
            IAgentDashboardService dashboardService,
            IAgentCustomerService agentCustService,
            IPolicyAssignmentService policyService,
            IInsurancePlanService planService,
            IClaimService claimService,
            IInvoiceService invoiceService,
            IPolicyEndorsementService endorsementService)
        {
            _dashboardService = dashboardService;
            _agentCustService = agentCustService;
            _policyService = policyService;
            _planService = planService;
            _claimService = claimService;
            _invoiceService = invoiceService;
            _endorsementService = endorsementService;
        }

        [HttpPost("review-endorsement/{id}")]
        public async Task<IActionResult> ReviewEndorsement(Guid id, [FromBody] ReviewEndorsementDto dto)
        {
            var agentIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(agentIdStr) || !Guid.TryParse(agentIdStr, out var agentId))
                return Unauthorized();

            var result = await _endorsementService.ReviewEndorsementAsync(agentId, id, dto);
            return Ok(result);
        }

        [HttpGet("pending-endorsements")]
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

        [HttpPost("create-customer")]
        public async Task<IActionResult> CreateCustomer([FromForm] AgentCreateCustomerDto dto)
        {
            var agentIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(agentIdStr) || !Guid.TryParse(agentIdStr, out var agentId))
                return Unauthorized();

            await _agentCustService.CreateCustomerByAgentAsync(agentId, dto);
            return Ok(new { message = "Customer created and documents uploaded successfully." });
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

        [HttpGet("summary")]
        public async Task<ActionResult<AgentDashboardSummaryDto>> GetSummary()
        {
            var agentIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(agentIdStr) || !Guid.TryParse(agentIdStr, out var agentId))
            {
                return Unauthorized();
            }

            var summary = await _dashboardService.GetSummaryAsync(agentId);
            return Ok(summary);
        }

        [HttpGet("my-customers")]
        public async Task<ActionResult<List<CorporateClientResponseDto>>> GetMyCustomers()
        {
            var agentIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(agentIdStr) || !Guid.TryParse(agentIdStr, out var agentId))
            {
                return Unauthorized();
            }

            var customers = await _dashboardService.GetMyCustomersAsync(agentId);
            return Ok(customers);
        }

        [HttpGet("my-policies")]
        public async Task<ActionResult<List<PolicyAssignmentResponseDto>>> GetMyPolicies()
        {
            var agentIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(agentIdStr) || !Guid.TryParse(agentIdStr, out var agentId))
            {
                return Unauthorized();
            }

            var policies = await _dashboardService.GetMyPoliciesAsync(agentId);
            return Ok(policies);
        }
    }
}
