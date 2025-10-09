using DermaKlinik.API.Application.DTOs.Email;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Text;

namespace DermaKlinik.API.Application.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<EmailResponseDto> SendContactEmailAsync(EmailRequestDto emailRequest)
        {
            try
            {
                var subject = $"İletişim Formu: {emailRequest.Subject}";
                var body = CreateContactEmailBody(emailRequest);
                
                return await SendEmailAsync("iletisim@drmehmetunal.com", subject, body, true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Contact email gönderilirken hata oluştu");
                return new EmailResponseDto
                {
                    Success = false,
                    Message = "E-posta gönderilirken bir hata oluştu",
                    ErrorDetails = ex.Message,
                    SentAt = DateTime.UtcNow
                };
            }
        }

        public async Task<EmailResponseDto> SendEmailAsync(string to, string subject, string body, bool isHtml = true)
        {
            try
            {
                var emailSettings = _configuration.GetSection("EmailSettings");

                var smtpHost = emailSettings["SmtpHost"];
                var smtpPort = int.Parse(emailSettings["SmtpPort"] ?? "587");
                var smtpUsername = emailSettings["SmtpUsername"];
                var smtpPassword = emailSettings["SmtpPassword"];
                var fromEmail = emailSettings["FromEmail"];
                var fromName = emailSettings["FromName"];
                var enableSsl = bool.Parse(emailSettings["EnableSsl"] ?? "true");

                if (string.IsNullOrEmpty(smtpHost) || string.IsNullOrEmpty(smtpUsername) || string.IsNullOrEmpty(smtpPassword))
                {
                    throw new InvalidOperationException("Email ayarları eksik");
                }

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(fromName, fromEmail));
                message.To.Add(new MailboxAddress("", to));
                message.Subject = subject;

                var bodyBuilder = new BodyBuilder();
                if (isHtml)
                {
                    bodyBuilder.HtmlBody = body;
                }
                else
                {
                    bodyBuilder.TextBody = body;
                }
                message.Body = bodyBuilder.ToMessageBody();

                using var client = new SmtpClient();
                
                // SSL/TLS seçeneklerini port'a göre belirle
                SecureSocketOptions sslOption;
                if (smtpPort == 465)
                {
                    sslOption = SecureSocketOptions.SslOnConnect;
                }
                else if (smtpPort == 587)
                {
                    sslOption = SecureSocketOptions.StartTls;
                }
                else
                {
                    sslOption = enableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.None;
                }

                // Bağlantı timeout'u ayarla
                client.Timeout = 30000; // 30 saniye

                await client.ConnectAsync(smtpHost, smtpPort, sslOption);
                await client.AuthenticateAsync(smtpUsername, smtpPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                _logger.LogInformation("E-posta başarıyla gönderildi: {To}", to);

                return new EmailResponseDto
                {
                    Success = true,
                    Message = "E-posta başarıyla gönderildi",
                    SentAt = DateTime.UtcNow
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "E-posta gönderilirken hata oluştu: {To}. Hata detayı: {ErrorDetails}", to, ex.Message);
                return new EmailResponseDto
                {
                    Success = false,
                    Message = "E-posta gönderilirken bir hata oluştu",
                    ErrorDetails = ex.Message,
                    SentAt = DateTime.UtcNow
                };
            }
        }

        public Task<EmailResponseDto> SendEmailWithTemplateAsync(string to, string subject, string templateName, object model)
        {
            // Template sistemi için gelecekte implementasyon yapılabilir
            return Task.FromException<EmailResponseDto>(new NotImplementedException("Template sistemi henüz implement edilmedi"));
        }

        private string CreateContactEmailBody(EmailRequestDto emailRequest)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<html>");
            sb.AppendLine("<head><style>");
            sb.AppendLine("body { font-family: Arial, sans-serif; line-height: 1.6; color: #333; }");
            sb.AppendLine(".header { background-color: #f4f4f4; padding: 20px; border-radius: 5px; }");
            sb.AppendLine(".content { padding: 20px; }");
            sb.AppendLine(".field { margin-bottom: 15px; }");
            sb.AppendLine(".label { font-weight: bold; color: #555; }");
            sb.AppendLine(".value { margin-top: 5px; }");
            sb.AppendLine("</style></head>");
            sb.AppendLine("<body>");
            sb.AppendLine("<div class='header'>");
            sb.AppendLine("<h2>İletişim Formu Mesajı</h2>");
            sb.AppendLine("<p>Web sitesinden yeni bir iletişim formu mesajı alındı.</p>");
            sb.AppendLine("</div>");
            sb.AppendLine("<div class='content'>");
            
            sb.AppendLine("<div class='field'>");
            sb.AppendLine("<div class='label'>Ad Soyad:</div>");
            sb.AppendLine($"<div class='value'>{emailRequest.Name}</div>");
            sb.AppendLine("</div>");

            sb.AppendLine("<div class='field'>");
            sb.AppendLine("<div class='label'>E-posta:</div>");
            sb.AppendLine($"<div class='value'>{emailRequest.Email}</div>");
            sb.AppendLine("</div>");

            if (!string.IsNullOrEmpty(emailRequest.Phone))
            {
                sb.AppendLine("<div class='field'>");
                sb.AppendLine("<div class='label'>Telefon:</div>");
                sb.AppendLine($"<div class='value'>{emailRequest.Phone}</div>");
                sb.AppendLine("</div>");
            }

            if (!string.IsNullOrEmpty(emailRequest.CompanyName))
            {
                sb.AppendLine("<div class='field'>");
                sb.AppendLine("<div class='label'>Şirket:</div>");
                sb.AppendLine($"<div class='value'>{emailRequest.CompanyName}</div>");
                sb.AppendLine("</div>");
            }

            sb.AppendLine("<div class='field'>");
            sb.AppendLine("<div class='label'>Konu:</div>");
            sb.AppendLine($"<div class='value'>{emailRequest.Subject}</div>");
            sb.AppendLine("</div>");

            sb.AppendLine("<div class='field'>");
            sb.AppendLine("<div class='label'>Mesaj:</div>");
            sb.AppendLine($"<div class='value'>{emailRequest.Message.Replace("\n", "<br>")}</div>");
            sb.AppendLine("</div>");

            sb.AppendLine("<div class='field'>");
            sb.AppendLine("<div class='label'>Gönderim Tarihi:</div>");
            sb.AppendLine($"<div class='value'>{DateTime.Now:dd.MM.yyyy HH:mm}</div>");
            sb.AppendLine("</div>");

            sb.AppendLine("</div>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            return sb.ToString();
        }
    }
}
