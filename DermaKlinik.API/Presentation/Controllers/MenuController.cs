using DermaKlinik.API.Application.DTOs.Menu;
using DermaKlinik.API.Application.Features.Menu.Commands;
using DermaKlinik.API.Application.Features.Menu.Queries;
using DermaKlinik.API.Application.Models.FilterModels;
using DermaKlinik.API.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DermaKlinik.API.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Hem JWT hem de API Key authentication'ı destekler
    public class MenuController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MenuController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<MenuDto>>> GetAll([FromQuery] PagingRequestModel request, [FromQuery] MenuFilter filters)
        {
            var query = new GetAllMenusQuery() { Request = request, Filters = filters };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MenuDto>> GetById(Guid id)
        {
            var query = new GetMenuByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<MenuDto>> Create([FromBody] CreateMenuDto createMenuDto)
        {
            var command = new CreateMenuCommand { CreateMenuDto = createMenuDto };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<MenuDto>> Update([FromBody] UpdateMenuDto updateMenuDto)
        {
            var command = new UpdateMenuCommand { UpdateMenuDto = updateMenuDto };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteMenuCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}