using DermaKlinik.API.Application.DTOs.GalleryImage;
using DermaKlinik.API.Application.Features.GalleryImage.Commands;
using DermaKlinik.API.Application.Features.GalleryImage.Queries;
using DermaKlinik.API.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DermaKlinik.API.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GalleryImageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GalleryImageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PagingRequestModel pagingRequest, [FromQuery] Guid? groupId)
        {
            var query = new GetAllGalleryImagesQuery
            {
                PagingRequest = pagingRequest,
                GroupId = groupId
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetGalleryImageByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGalleryImageDto createDto)
        {
            var command = new CreateGalleryImageCommand
            {
                CreateGalleryImageDto = createDto
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateGalleryImageDto updateDto)
        {
            var command = new UpdateGalleryImageCommand
            {
                Id = id,
                UpdateGalleryImageDto = updateDto
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteGalleryImageCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}/hard")]
        public async Task<IActionResult> HardDelete(Guid id)
        {
            var command = new HardDeleteGalleryImageCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("add-to-group")]
        public async Task<IActionResult> AddToGroup([FromBody] AddToGroupCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("remove-from-group")]
        public async Task<IActionResult> RemoveFromGroup([FromBody] RemoveFromGroupCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("update-order")]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateImageOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
