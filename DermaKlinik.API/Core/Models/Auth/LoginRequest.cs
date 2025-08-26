using System.ComponentModel.DataAnnotations;

namespace DermaKlinik.API.Core.Models.Auth
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string UserName { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}