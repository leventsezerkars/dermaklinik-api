using DermaKlinik.API.Application.DTOs.Email;

namespace DermaKlinik.API.Application.Services.Email
{
    public interface IEmailService
    {
        Task<EmailResponseDto> SendContactEmailAsync(EmailRequestDto emailRequest);
        Task<EmailResponseDto> SendEmailAsync(string to, string subject, string body, bool isHtml = true);
        Task<EmailResponseDto> SendEmailWithTemplateAsync(string to, string subject, string templateName, object model);
        Task<EmailResponseDto> SendAutoReplyEmailAsync(string to, string name);
    }
}
