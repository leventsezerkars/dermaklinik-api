using DermaKlinik.API.Core.Models;
using DermaKlinik.API.Application.Features.Menus.Commands;
using DermaKlinik.API.Application.Features.Menus.Queries;
using DermaKlinik.API.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DermaKlinik.API.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MenuController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MenuController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PaginatedList<Menu>>>> GetAll([FromQuery] PagingParams pagingParams)
        {
            var query = new GetAllMenusQuery 
            { 
                PageNumber = pagingParams.PageNumber, 
                PageSize = pagingParams.PageSize 
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Menu>>> GetById(Guid id)
        {
            var query = new GetMenuByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("root")]
        public async Task<ActionResult<ApiResponse<IEnumerable<Menu>>>> GetRootMenus()
        {
            var query = new GetRootMenusQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("active")]
        public async Task<ActionResult<ApiResponse<IEnumerable<Menu>>>> GetActiveMenus()
        {
            var query = new GetActiveMenusQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("permission/{permission}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<Menu>>>> GetMenusByPermission(string permission)
        {
            var query = new GetMenusByPermissionQuery { Permission = permission };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<Menu>>> Create(CreateMenuCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<Menu>>> Update(Guid id, UpdateMenuCommand command)
        {
            if (id != command.Id)
                return BadRequest(ApiResponse<Menu>.ErrorResult("ID'ler eşleşmiyor."));

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(Guid id)
        {
            var command = new DeleteMenuCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
} 