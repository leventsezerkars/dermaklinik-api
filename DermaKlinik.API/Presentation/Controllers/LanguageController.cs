using DermaKlinik.API.Application.DTOs.Language;
using DermaKlinik.API.Application.Features.Language.Commands.CreateLanguage;
using DermaKlinik.API.Application.Features.Language.Commands.UpdateLanguage;
using DermaKlinik.API.Application.Features.Language.Queries.GetAllLanguages;
using DermaKlinik.API.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DermaKlinik.API.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class LanguageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LanguageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<LanguageDto>>>> GetAll()
        {
            var query = new GetAllLanguagesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<LanguageDto>>> Create([FromBody] CreateLanguageDto createLanguageDto)
        {
            var command = new CreateLanguageCommand { CreateLanguageDto = createLanguageDto };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse<LanguageDto>>> Update([FromBody] UpdateLanguageDto updateLanguageDto)
        {
            var command = new UpdateLanguageCommand
            {
                UpdateLanguageDto = updateLanguageDto
            };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
} 