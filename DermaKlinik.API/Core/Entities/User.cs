using System.ComponentModel.DataAnnotations;

namespace DermaKlinik.API.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        
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
        
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastLoginAt { get; set; }
    }
} 