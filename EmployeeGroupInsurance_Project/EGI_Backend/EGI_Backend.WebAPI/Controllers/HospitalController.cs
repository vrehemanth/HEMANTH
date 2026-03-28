using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EGI_Backend.Application.Interfaces;
using EGI_Backend.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EGI_Backend.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HospitalController : ControllerBase
    {
        private readonly IHospitalService _hospitalService;

        public HospitalController(IHospitalService hospitalService)
        {
            _hospitalService = hospitalService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Customer,ClaimsOfficer")]
        public async Task<ActionResult<List<HospitalDto>>> GetAll()
        {
            var hospitals = await _hospitalService.GetAllHospitalsAsync();
            return Ok(hospitals);
        }

        [HttpGet("network")]
        [AllowAnonymous] // Allow anyone to see network hospitals or keep it authorized? User said "customer hr can see it"
        public async Task<ActionResult<List<HospitalDto>>> GetNetwork()
        {
            var hospitals = await _hospitalService.GetNetworkHospitalsAsync();
            return Ok(hospitals);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<HospitalDto>> GetById(Guid id)
        {
            var hospital = await _hospitalService.GetHospitalByIdAsync(id);
            if (hospital == null) return NotFound();
            return Ok(hospital);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<HospitalDto>> Create(CreateUpdateHospitalDto dto)
        {
            var result = await _hospitalService.CreateHospitalAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<HospitalDto>> Update(Guid id, CreateUpdateHospitalDto dto)
        {
            var result = await _hospitalService.UpdateHospitalAsync(id, dto);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _hospitalService.DeleteHospitalAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
