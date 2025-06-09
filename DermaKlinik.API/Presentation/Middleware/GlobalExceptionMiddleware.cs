using DermaKlinik.API.Core.Extensions;
using DermaKlinik.API.Core.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

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
                case ValidationException validationEx:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response = ApiResponse.ErrorResult(
                        validationEx.Errors.Select(x => x.ErrorMessage).ToList().Join(","),
                        HttpStatusCode.BadRequest
                    );
                    break;

                case DbUpdateException dbEx:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response = ApiResponse.ErrorResult(
                        "Veritabanı işlemi sırasında bir hata oluştu.",
                        HttpStatusCode.BadRequest
                    );
                    _logger.LogError(dbEx, "Veritabanı hatası: {Message}", dbEx.Message);
                    break;

                case UnauthorizedAccessException:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    response = ApiResponse.ErrorResult(
                        "Bu işlem için yetkiniz bulunmamaktadır.",
                        HttpStatusCode.Unauthorized
                    );
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response = ApiResponse.ErrorResult(
                        "Beklenmeyen bir hata oluştu.",
                        HttpStatusCode.InternalServerError
                    );
                    _logger.LogError(exception, "Beklenmeyen hata: {Message}", exception.Message);
                    break;
            }

            var result = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(result);
        }
    }
}