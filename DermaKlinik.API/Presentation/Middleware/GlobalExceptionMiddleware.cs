using DermaKlinik.API.Core.Extensions;
using DermaKlinik.API.Core.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace DermaKlinik.API.Presentation.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = new ApiResponse();

            switch (exception)
            {
                case FluentValidation.ValidationException validationEx:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response = HandleValidationException(validationEx);
                    break;

                case DbUpdateException dbEx:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response = ApiResponse.ErrorResult(
                        "Veritabanı işlemi sırasında bir hata oluştu. -> " + exception.MessagesStr(),
                        HttpStatusCode.BadRequest
                    );
                    _logger.LogError(dbEx, "Veritabanı hatası: {Message}", dbEx.Message);
                    break;

                case UnauthorizedAccessException unex:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    response = ApiResponse.ErrorResult(
                        "Bu işlem için yetkiniz bulunmamaktadır. -> " + exception.MessagesStr(),
                        HttpStatusCode.Unauthorized
                    );
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response = ApiResponse.ErrorResult(
                        "Beklenmeyen bir hata oluştu. -> " + exception.MessagesStr(),
                        HttpStatusCode.InternalServerError
                    );
                    _logger.LogError(exception, "Beklenmeyen hata: {Message}", exception.Message);
                    break;
            }

            var result = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });
            await context.Response.WriteAsync(result);
        }

        private ApiResponse HandleValidationException(FluentValidation.ValidationException validationEx)
        {
            var errorMessages = validationEx.Errors.Select(error => 
                $"{error.PropertyName}: {GetUserFriendlyMessage(error.ErrorMessage)}"
            ).ToList();

            return ApiResponse.ErrorResult(
                "Girilen verilerde hatalar bulundu. Lütfen kontrol ediniz.\n" + string.Join("\n", errorMessages),
                HttpStatusCode.BadRequest,
                errorMessages
            );
        }

        private string GetUserFriendlyMessage(string errorMessage)
        {
            // FluentValidation hata mesajlarını daha kullanıcı dostu hale getir
            return errorMessage switch
            {
                var msg when msg.Contains("required") => "Bu alan zorunludur.",
                var msg when msg.Contains("not be empty") => "Bu alan boş olamaz.",
                var msg when msg.Contains("invalid") => "Geçersiz değer girildi.",
                var msg when msg.Contains("length") => "Girilen değer uygun uzunlukta değil.",
                var msg when msg.Contains("email") => "Geçerli bir e-posta adresi giriniz.",
                var msg when msg.Contains("phone") => "Geçerli bir telefon numarası giriniz.",
                var msg when msg.Contains("date") => "Geçerli bir tarih giriniz.",
                var msg when msg.Contains("numeric") => "Sadece sayısal değer giriniz.",
                var msg when msg.Contains("unique") => "Bu değer zaten kullanılmaktadır.",
                _ => errorMessage // Eğer özel bir çeviri yoksa orijinal mesajı döndür
            };
        }
    }
}