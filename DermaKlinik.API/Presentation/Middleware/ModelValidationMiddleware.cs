using DermaKlinik.API.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace DermaKlinik.API.Presentation.Middleware
{
    public class ModelValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ModelValidationMiddleware> _logger;

        public ModelValidationMiddleware(RequestDelegate next, ILogger<ModelValidationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Model validation hatalarını yakalamak için özel bir filter kullanacağız
            await _next(context);
        }
    }

    public class ModelValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errorMessages = new List<string>();

                foreach (var modelState in context.ModelState)
                {
                    var fieldName = modelState.Key;
                    var errors = modelState.Value.Errors;

                    foreach (var error in errors)
                    {
                        errorMessages.Add($"{fieldName}: {GetUserFriendlyMessage(error.ErrorMessage)}");
                    }
                }

                var response = ApiResponse.ErrorResult(
                    "Girilen verilerde hatalar bulundu. Lütfen kontrol ediniz.\n" + string.Join("\n", errorMessages),
                    System.Net.HttpStatusCode.BadRequest,
                    errorMessages
                );

                context.Result = new BadRequestObjectResult(response);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Action tamamlandıktan sonra yapılacak işlemler
        }

        private string GetUserFriendlyMessage(string errorMessage)
        {
            if (string.IsNullOrEmpty(errorMessage))
                return "Geçersiz değer.";

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
                var msg when msg.Contains("The field") && msg.Contains("is required") => "Bu alan zorunludur.",
                var msg when msg.Contains("The value") && msg.Contains("is not valid") => "Geçersiz değer girildi.",
                _ => errorMessage
            };
        }
    }
}
