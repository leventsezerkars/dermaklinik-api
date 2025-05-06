using Microsoft.AspNetCore.Mvc;
using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Core.Models;
using DermaKlinik.API.Application.Features.Patients.Queries;
using DermaKlinik.API.Application.Features.Patients.Commands;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            return Ok(response);
        }

        // GET: api/Patients/active
        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<Patient>>> GetActivePatients()
        {
            var patients = await _mediator.Send(new GetActivePatientsQuery());
            return Ok(patients);
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Patient>>> GetPatient(int id)
        {
            var query = new GetPatientByIdQuery(id);
            var response = await _mediator.Send(query);

            if (!response.Success)
                return StatusCode(response.StatusCode, response);

            return Ok(response);
        }

        // POST: api/Patients
        [HttpPost]
        public async Task<ActionResult<ApiResponse<Patient>>> CreatePatient(CreatePatientCommand command)
        {
            var response = await _mediator.Send(command);

            if (!response.Success)
                return StatusCode(response.StatusCode, response);

            return StatusCode(201, response);
        }

        // PUT: api/Patients/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<Patient>>> UpdatePatient(int id, UpdatePatientCommand command)
        {
            if (id != command.Id)
                return BadRequest(ApiResponse<Patient>.ErrorResult("ID uyuşmazlığı"));

            var response = await _mediator.Send(command);

            if (!response.Success)
                return StatusCode(response.StatusCode, response);

            return Ok(response);
        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeletePatient(int id)
        {
            var command = new DeletePatientCommand(id);
            var response = await _mediator.Send(command);

            if (!response.Success)
                return StatusCode(response.StatusCode, response);

            return Ok(response);
        }
    }
} 