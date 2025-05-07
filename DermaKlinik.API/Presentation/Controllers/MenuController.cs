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
            if (pagingParams == null)
                return BadRequest(ApiResponse<PaginatedList<Menu>>.ErrorResult("Geçersiz sayfalama parametreleri"));

            var query = new GetAllMenusQuery 
            { 
                PageNumber = pagingParams.PageNumber, 
                PageSize = pagingParams.PageSize 
            };
            var result = await _mediator.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Menu>>> GetById(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest(ApiResponse<Menu>.ErrorResult("Geçersiz ID"));

            var query = new GetMenuByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("root")]
        public async Task<ActionResult<ApiResponse<IEnumerable<Menu>>>> GetRootMenus()
        {
            var query = new GetRootMenusQuery();
            var result = await _mediator.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("active")]
        public async Task<ActionResult<ApiResponse<IEnumerable<Menu>>>> GetActiveMenus()
        {
            var query = new GetActiveMenusQuery();
            var result = await _mediator.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("permission/{permission}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<Menu>>>> GetMenusByPermission(string permission)
        {
            if (string.IsNullOrEmpty(permission))
                return BadRequest(ApiResponse<IEnumerable<Menu>>.ErrorResult("Geçersiz yetki parametresi"));

            var query = new GetMenusByPermissionQuery { Permission = permission };
            var result = await _mediator.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<Menu>>> Create(CreateMenuCommand command)
        {
            if (command == null)
                return BadRequest(ApiResponse<Menu>.ErrorResult("Geçersiz istek"));

            var result = await _mediator.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<Menu>>> Update(Guid id, UpdateMenuCommand command)
        {
            if (id == Guid.Empty)
                return BadRequest(ApiResponse<Menu>.ErrorResult("Geçersiz ID"));

            if (command == null)
                return BadRequest(ApiResponse<Menu>.ErrorResult("Geçersiz istek"));

            if (id != command.Id)
                return BadRequest(ApiResponse<Menu>.ErrorResult("ID'ler eşleşmiyor."));

            var result = await _mediator.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest(ApiResponse<bool>.ErrorResult("Geçersiz ID"));

            var command = new DeleteMenuCommand { Id = id };
            var result = await _mediator.Send(command);
            return StatusCode(result.StatusCode, result);
        }
    }
} 