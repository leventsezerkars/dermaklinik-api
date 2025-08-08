using DermaKlinik.API.Application.DTOs.CompanyInfo;
using DermaKlinik.API.Application.Features.CompanyInfo.Commands;
using DermaKlinik.API.Application.Features.CompanyInfo.Queries;
using DermaKlinik.API.Application.Models.FilterModels;
using DermaKlinik.API.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DermaKlinik.API.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CompanyInfoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompanyInfoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Tüm şirket bilgilerini getirir
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<CompanyInfoDto>>>> GetAll()
        {
            var query = new GetAllCompanyInfosQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Sayfalama ve filtreleme ile şirket bilgilerini getirir
        /// </summary>
        [HttpGet("paged")]
        public async Task<ActionResult<ApiResponse<List<CompanyInfoDto>>>> GetAllPaged([FromQuery] PagingRequestModel request, [FromQuery] CompanyInfoFilter filters)
        {
            var query = new GetAllCompanyInfosPagedQuery
            {
                Request = request,
                Filters = filters
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Aktif şirket bilgilerini getirir
        /// </summary>
        [HttpGet("active")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<List<CompanyInfoDto>>>> GetActive()
        {
            var query = new GetActiveCompanyInfosQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Aktif şirket bilgisini getirir (tek kayıt)
        /// </summary>
        [HttpGet("active/single")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<CompanyInfoDto>>> GetActiveCompanyInfo()
        {
            var query = new GetActiveCompanyInfoQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// ID'ye göre şirket bilgisi getirir
        /// </summary>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ApiResponse<CompanyInfoDto>>> GetById(Guid id)
        {
            var query = new GetCompanyInfoByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Yeni şirket bilgisi oluşturur
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<CompanyInfoDto>>> Create([FromBody] CreateCompanyInfoDto createCompanyInfoDto)
        {
            var command = new CreateCompanyInfoCommand { CreateCompanyInfoDto = createCompanyInfoDto };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Şirket bilgisini günceller
        /// </summary>
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ApiResponse<CompanyInfoDto>>> Update(Guid id, [FromBody] UpdateCompanyInfoDto updateCompanyInfoDto)
        {
            var command = new UpdateCompanyInfoCommand 
            { 
                Id = id, 
                UpdateCompanyInfoDto = updateCompanyInfoDto 
            };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Şirket bilgisini siler (soft delete)
        /// </summary>
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(Guid id)
        {
            var command = new DeleteCompanyInfoCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Şirket bilgisini kalıcı olarak siler (hard delete)
        /// </summary>
        [HttpDelete("{id:guid}/hard")]
        public async Task<ActionResult<ApiResponse<bool>>> HardDelete(Guid id)
        {
            var command = new HardDeleteCompanyInfoCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Şirket adının benzersiz olup olmadığını kontrol eder
        /// </summary>
        [HttpGet("check-name-unique")]
        public async Task<ActionResult<ApiResponse<bool>>> IsNameUnique([FromQuery] string name, [FromQuery] Guid? excludeId = null)
        {
            var query = new CheckCompanyInfoNameUniqueQuery 
            { 
                Name = name, 
                ExcludeId = excludeId 
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// E-posta adresinin benzersiz olup olmadığını kontrol eder
        /// </summary>
        [HttpGet("check-email-unique")]
        public async Task<ActionResult<ApiResponse<bool>>> IsEmailUnique([FromQuery] string email, [FromQuery] Guid? excludeId = null)
        {
            var query = new CheckCompanyInfoEmailUniqueQuery 
            { 
                Email = email, 
                ExcludeId = excludeId 
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Şirket bilgisinin var olup olmadığını kontrol eder
        /// </summary>
        [HttpGet("{id:guid}/exists")]
        public async Task<ActionResult<ApiResponse<bool>>> Exists(Guid id)
        {
            var query = new CheckCompanyInfoExistsQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
} 