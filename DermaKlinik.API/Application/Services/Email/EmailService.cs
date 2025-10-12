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
                
                // Ana iletişim mailini gönder
                var mainEmailResult = await SendEmailAsync("iletisim@drmehmetunal.com", subject, body, true);
                
                // Ana mail başarılıysa otomatik yanıt maili gönder
                if (mainEmailResult.Success)
                {
                    try
                    {
                        await SendAutoReplyEmailAsync(emailRequest.Email, emailRequest.Name);
                        _logger.LogInformation("Otomatik yanıt maili başarıyla gönderildi: {Email}", emailRequest.Email);
                    }
                    catch (Exception autoReplyEx)
                    {
                        // Otomatik yanıt maili başarısız olsa bile ana mail gönderilmiş sayılır
                        _logger.LogWarning(autoReplyEx, "Otomatik yanıt maili gönderilemedi: {Email}", emailRequest.Email);
                    }
                }
                
                return mainEmailResult;
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

        public async Task<EmailResponseDto> SendAutoReplyEmailAsync(string to, string name)
        {
            try
            {
                var subject = "Mesajınız Tarafımıza Ulaşmıştır - Dr. Mehmet Unal Dermotoloji Kliniği";
                var body = CreateAutoReplyEmailBody(name);
                
                return await SendEmailAsync(to, subject, body, true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Otomatik yanıt emaili gönderilirken hata oluştu: {To}", to);
                return new EmailResponseDto
                {
                    Success = false,
                    Message = "Otomatik yanıt e-postası gönderilirken bir hata oluştu",
                    ErrorDetails = ex.Message,
                    SentAt = DateTime.UtcNow
                };
            }
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

        private string CreateAutoReplyEmailBody(string name)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<html>");
            sb.AppendLine("<head><style>");
            sb.AppendLine("body { font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; line-height: 1.6; color: #333; margin: 0; padding: 0; background-color: #f8f9fa; }");
            sb.AppendLine(".container { max-width: 600px; margin: 0 auto; background-color: #ffffff; border-radius: 10px; overflow: hidden; box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1); }");
            sb.AppendLine(".header { background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); color: white; padding: 30px 20px; text-align: center; }");
            sb.AppendLine(".header h1 { margin: 0; font-size: 28px; font-weight: 300; }");
            sb.AppendLine(".content { padding: 40px 30px; }");
            sb.AppendLine(".greeting { font-size: 18px; margin-bottom: 20px; color: #2c3e50; }");
            sb.AppendLine(".message { font-size: 16px; margin-bottom: 25px; color: #555; line-height: 1.8; }");
            sb.AppendLine(".highlight { background-color: #e8f4fd; padding: 20px; border-left: 4px solid #3498db; margin: 25px 0; border-radius: 0 5px 5px 0; }");
            sb.AppendLine(".highlight p { margin: 0; font-weight: 500; color: #2c3e50; }");
            sb.AppendLine(".contact-info { background-color: #f8f9fa; padding: 20px; border-radius: 8px; margin-top: 30px; }");
            sb.AppendLine(".contact-info h3 { margin: 0 0 15px 0; color: #2c3e50; font-size: 18px; }");
            sb.AppendLine(".contact-info p { margin: 5px 0; color: #666; }");
            sb.AppendLine(".footer { background-color: #2c3e50; color: white; padding: 20px; text-align: center; font-size: 14px; }");
            sb.AppendLine(".footer p { margin: 5px 0; }");
            sb.AppendLine(".logo { font-size: 24px; font-weight: bold; margin-bottom: 10px; }");
            sb.AppendLine("</style></head>");
            sb.AppendLine("<body>");
            sb.AppendLine("<div class='container'>");
            
            // Header
            sb.AppendLine("<div class='header'>");
            sb.AppendLine("<div class='logo'>DermaKlinik</div>");
            sb.AppendLine("<h1>Mesajınız Alındı</h1>");
            sb.AppendLine("</div>");
            
            // Content
            sb.AppendLine("<div class='content'>");
            sb.AppendLine($"<div class='greeting'>Sayın {name},</div>");
            
            sb.AppendLine("<div class='message'>");
            sb.AppendLine("<p>İletişim formu aracılığıyla gönderdiğiniz mesajınız tarafımıza başarıyla ulaşmıştır.</p>");
            sb.AppendLine("<p>Mesajınızı en kısa sürede inceleyerek size dönüş sağlayacağız. Değerli vaktinizi ayırdığınız için teşekkür ederiz.</p>");
            sb.AppendLine("</div>");
            
            sb.AppendLine("<div class='highlight'>");
            sb.AppendLine("<p>📧 Mesajınız en geç 24 saat içinde yanıtlanacaktır.</p>");
            sb.AppendLine("</div>");
            
            sb.AppendLine("<div class='contact-info'>");
            sb.AppendLine("<h3>📞 Acil Durumlar İçin</h3>");
            sb.AppendLine("<p>Eğer acil bir durumunuz varsa, lütfen doğrudan telefon numaramızı arayın.</p>");
            sb.AppendLine("<p><strong>Telefon:</strong> +90 (212) 123 45 67</p>");
            sb.AppendLine("<p><strong>E-posta:</strong> info@dermaklinik.com</p>");
            sb.AppendLine("</div>");
            
            sb.AppendLine("</div>");
            
            // Footer
            sb.AppendLine("<div class='footer'>");
            sb.AppendLine("<p><strong>DermaKlinik</strong></p>");
            sb.AppendLine("<p>Profesyonel Dermatoloji Hizmetleri</p>");
            sb.AppendLine($"<p>Bu e-posta {DateTime.Now:dd.MM.yyyy HH:mm} tarihinde otomatik olarak gönderilmiştir.</p>");
            sb.AppendLine("</div>");
            
            sb.AppendLine("</div>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            return sb.ToString();
        }
    }
}
