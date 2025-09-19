using DermaKlinik.API.Application.DTOs.Blog;
using DermaKlinik.API.Application.Features.Blog.Commands;
using DermaKlinik.API.Application.Features.Blog.Queries;
using DermaKlinik.API.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DermaKlinik.API.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Hem JWT hem de API Key authentication'ı destekler
    public class BlogController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BlogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<BlogDto>>> GetAll(
            [FromQuery] PagingRequestModel pagingRequest,
            [FromQuery] Guid? categoryId = null,
            [FromQuery] Guid? languageId = null)
        {
            var query = new GetAllBlogsQuery 
            { 
                PagingRequest = pagingRequest,
                CategoryId = categoryId,
                LanguageId = languageId
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BlogDto>> GetById(Guid id, [FromQuery] Guid? languageId = null)
        {
            var query = new GetBlogByIdQuery { Id = id, LanguageId = languageId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("slug/{slug}")]
        public async Task<ActionResult<BlogDto>> GetBySlug(string slug, [FromQuery] Guid? languageId = null)
        {
            var query = new GetBlogBySlugQuery { Slug = slug, LanguageId = languageId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<BlogDto>> Create([FromBody] CreateBlogDto createBlogDto)
        {
            var command = new CreateBlogCommand { CreateBlogDto = createBlogDto };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<BlogDto>> Update([FromBody] UpdateBlogDto updateBlogDto)
        {
            var command = new UpdateBlogCommand { UpdateBlogDto = updateBlogDto };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteBlogCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPost("{id}/increment-view")]
        public async Task<IActionResult> IncrementViewCount(Guid id)
        {
            var command = new IncrementViewCountCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
