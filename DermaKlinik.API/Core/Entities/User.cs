using System.ComponentModel.DataAnnotations;

namespace DermaKlinik.API.Core.Entities
{
    public class User : AuditableEntity
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string PasswordHash { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string Role { get; set; } = "User";


        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        public DateTime? LastLoginDate { get; set; }

    }
}