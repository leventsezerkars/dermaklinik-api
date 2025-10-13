using DermaKlinik.API.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DermaKlinik.API.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpPost("validation-test")]
        public IActionResult TestValidation([FromBody] TestModel model)
        {
            return Ok(ApiResponse.SuccessResult(model, "Validation başarılı"));
        }
    }

    public class TestModel
    {
        [Required(ErrorMessage = "Name alanı zorunludur")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name 2-50 karakter arasında olmalıdır")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email alanı zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz")]
        public string Email { get; set; }

        [Range(18, 100, ErrorMessage = "Yaş 18-100 arasında olmalıdır")]
        public int Age { get; set; }
    }
}
