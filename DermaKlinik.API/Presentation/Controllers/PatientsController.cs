using Microsoft.AspNetCore.Mvc;
using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Core.Models;
using DermaKlinik.API.Application.Features.Patients.Queries;
using DermaKlinik.API.Application.Features.Patients.Commands;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace DermaKlinik.API.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PatientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Patients
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<Patient>>>> GetPatients()
        {
            var query = new GetAllPatientsQuery();
            var response = await _mediator.Send(query);
            return StatusCode(response.StatusCode, response);
        }

        // GET: api/Patients/active
        [HttpGet("active")]
        public async Task<ActionResult<ApiResponse<IEnumerable<Patient>>>> GetActivePatients()
        {
            var query = new GetActivePatientsQuery();
            var response = await _mediator.Send(query);
            return StatusCode(response.StatusCode, response);
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Patient>>> GetPatient(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest(ApiResponse<Patient>.ErrorResult("Geçersiz ID"));

            var query = new GetPatientByIdQuery(id);
            var response = await _mediator.Send(query);
            return StatusCode(response.StatusCode, response);
        }

        // POST: api/Patients
        [HttpPost]
        public async Task<ActionResult<ApiResponse<Patient>>> CreatePatient(CreatePatientCommand command)
        {
            if (command == null)
                return BadRequest(ApiResponse<Patient>.ErrorResult("Geçersiz istek"));

            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }

        // PUT: api/Patients/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<Patient>>> Update(Guid id, UpdatePatientCommand command)
        {
            if (id == Guid.Empty)
                return BadRequest(ApiResponse<Patient>.ErrorResult("Geçersiz ID"));

            if (command == null)
                return BadRequest(ApiResponse<Patient>.ErrorResult("Geçersiz istek"));

            if (id != command.Id)
                return BadRequest(ApiResponse<Patient>.ErrorResult("ID uyuşmazlığı"));

            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeletePatient(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest(ApiResponse<bool>.ErrorResult("Geçersiz ID"));

            var command = new DeletePatientCommand(id);
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }
    }
} 