using DermaKlinik.API.Application.DTOs.Email;
using DermaKlinik.API.Application.Services.Email;
using DermaKlinik.API.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DermaKlinik.API.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<EmailController> _logger;

        public EmailController(IEmailService emailService, ILogger<EmailController> logger)
        {
            _emailService = emailService;
            _logger = logger;
        }

        /// <summary>
        /// İletişim formu e-postası gönderir
        /// </summary>
        [HttpPost("contact")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<EmailResponseDto>>> SendContactEmail([FromBody] EmailRequestDto emailRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse<EmailResponseDto>
                    {
                        Result = false,
                        Message = "Geçersiz veri",
                        Data = null,
                        ErrorMessage = string.Join(", ", ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage))
                    });
                }

                var result = await _emailService.SendContactEmailAsync(emailRequest);
                
                if (result.Success)
                {
                    return Ok(new ApiResponse<EmailResponseDto>
                    {
                        Result = true,
                        Message = "E-posta başarıyla gönderildi",
                        Data = result
                    });
                }
                else
                {
                    return BadRequest(new ApiResponse<EmailResponseDto>
                    {
                        Result = false,
                        Message = result.Message,
                        Data = result
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Contact email gönderilirken hata oluştu");
                return StatusCode(500, new ApiResponse<EmailResponseDto>
                {
                    Result = false,
                    Message = "Sunucu hatası oluştu",
                    Data = null
                });
            }
        }

        /// <summary>
        /// Test e-postası gönderir (sadece development için)
        /// </summary>
        [HttpPost("test")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<EmailResponseDto>>> SendTestEmail()
        {
            try
            {
                var testEmail = new EmailRequestDto
                {
                    Name = "Test Kullanıcı",
                    Email = "leventsezer2011@gmail.com",
                    Subject = "Test E-postası",
                    Message = "Bu bir test e-postasıdır.",
                    Phone = "+90 555 123 45 67"
                };

                var result = await _emailService.SendContactEmailAsync(testEmail);
                
                return Ok(new ApiResponse<EmailResponseDto>
                {
                    Result = result.Success,
                    Message = result.Success ? "Test e-postası gönderildi" : result.Message,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Test email gönderilirken hata oluştu");
                return StatusCode(500, new ApiResponse<EmailResponseDto>
                {
                    Result = false,
                    Message = "Test e-postası gönderilirken hata oluştu",
                    Data = null
                });
            }
        }
    }
}
