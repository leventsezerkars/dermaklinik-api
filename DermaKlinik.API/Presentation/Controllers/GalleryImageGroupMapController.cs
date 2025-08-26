using DermaKlinik.API.Application.DTOs.GalleryImageGroupMap;
using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DermaKlinik.API.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GalleryImageGroupMapController : ControllerBase
    {
        private readonly IGalleryImageGroupMapService _mapService;

        public GalleryImageGroupMapController(IGalleryImageGroupMapService mapService)
        {
            _mapService = mapService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PagingRequestModel pagingRequest)
        {
            var result = await _mapService.GetAllAsync(pagingRequest);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mapService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGalleryImageGroupMapDto createDto)
        {
            var result = await _mapService.CreateAsync(createDto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateGalleryImageGroupMapDto updateDto)
        {
            var result = await _mapService.UpdateAsync(id, updateDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mapService.DeleteAsync(id);
            return Ok(new { success = true });
        }

        [HttpDelete("{id}/hard")]
        public async Task<IActionResult> HardDelete(Guid id)
        {
            await _mapService.HardDeleteAsync(id);
            return Ok(new { success = true });
        }

        [HttpGet("by-image/{imageId}")]
        public async Task<IActionResult> GetByImageId(Guid imageId)
        {
            var result = await _mapService.GetByImageIdAsync(imageId);
            return Ok(result);
        }

        [HttpGet("by-group/{groupId}")]
        public async Task<IActionResult> GetByGroupId(Guid groupId)
        {
            var result = await _mapService.GetByGroupIdAsync(groupId);
            return Ok(result);
        }

        [HttpPut("update-sort-order")]
        public async Task<IActionResult> UpdateSortOrder([FromBody] UpdateSortOrderDto updateDto)
        {
            await _mapService.UpdateSortOrderAsync(updateDto.ImageId, updateDto.GroupId, updateDto.NewSortOrder);
            return Ok(new { success = true });
        }
    }
}
