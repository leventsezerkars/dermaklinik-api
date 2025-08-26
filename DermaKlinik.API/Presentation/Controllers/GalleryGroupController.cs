using DermaKlinik.API.Application.DTOs.GalleryGroup;
using DermaKlinik.API.Application.Features.GalleryGroup.Commands;
using DermaKlinik.API.Application.Features.GalleryGroup.Queries;
using DermaKlinik.API.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DermaKlinik.API.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GalleryGroupController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GalleryGroupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PagingRequestModel pagingRequest)
        {
            var query = new GetAllGalleryGroupsQuery
            {
                PagingRequest = pagingRequest
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetGalleryGroupByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}/images")]
        public async Task<IActionResult> GetImagesByGroup(Guid id, [FromQuery] PagingRequestModel pagingRequest)
        {
            var query = new GetImagesByGroupQuery
            {
                GroupId = id,
                PagingRequest = pagingRequest
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGalleryGroupDto createDto)
        {
            var command = new CreateGalleryGroupCommand
            {
                CreateGalleryGroupDto = createDto
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateGalleryGroupDto updateDto)
        {
            var command = new UpdateGalleryGroupCommand
            {
                Id = id,
                UpdateGalleryGroupDto = updateDto
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteGalleryGroupCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}/hard")]
        public async Task<IActionResult> HardDelete(Guid id)
        {
            var command = new HardDeleteGalleryGroupCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
