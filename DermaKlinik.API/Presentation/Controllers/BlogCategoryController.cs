using DermaKlinik.API.Application.DTOs.BlogCategory;
using DermaKlinik.API.Application.Features.BlogCategory.Commands;
using DermaKlinik.API.Application.Features.BlogCategory.Queries;
using DermaKlinik.API.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DermaKlinik.API.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class BlogCategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BlogCategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<BlogCategoryDto>>> GetAll([FromQuery] PagingRequestModel pagingRequest)
        {
            var query = new GetAllBlogCategoriesQuery { PagingRequest = pagingRequest };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BlogCategoryDto>> GetById(Guid id)
        {
            var query = new GetBlogCategoryByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<BlogCategoryDto>> Create([FromBody] CreateBlogCategoryDto createBlogCategoryDto)
        {
            var command = new CreateBlogCategoryCommand { CreateBlogCategoryDto = createBlogCategoryDto };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<BlogCategoryDto>> Update([FromBody] UpdateBlogCategoryDto updateBlogCategoryDto)
        {
            var command = new UpdateBlogCategoryCommand { UpdateBlogCategoryDto = updateBlogCategoryDto };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteBlogCategoryCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
