using System.Net;
using System.Text.Json;
using DermaKlinik.API.Core.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

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
            var response = new ApiResponse<object>();

            switch (exception)
            {
                case ValidationException validationEx:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response = ApiResponse<object>.ErrorResult(
                        validationEx.Errors.Select(x => x.ErrorMessage).ToList(),
                        (int)HttpStatusCode.BadRequest
                    );
                    break;

                case DbUpdateException dbEx:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response = ApiResponse<object>.ErrorResult(
                        "Veritabanı işlemi sırasında bir hata oluştu.",
                        (int)HttpStatusCode.BadRequest
                    );
                    _logger.LogError(dbEx, "Veritabanı hatası: {Message}", dbEx.Message);
                    break;

                case UnauthorizedAccessException:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    response = ApiResponse<object>.ErrorResult(
                        "Bu işlem için yetkiniz bulunmamaktadır.",
                        (int)HttpStatusCode.Unauthorized
                    );
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response = ApiResponse<object>.ErrorResult(
                        "Beklenmeyen bir hata oluştu.",
                        (int)HttpStatusCode.InternalServerError
                    );
                    _logger.LogError(exception, "Beklenmeyen hata: {Message}", exception.Message);
                    break;
            }

            var result = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(result);
        }
    }
} 