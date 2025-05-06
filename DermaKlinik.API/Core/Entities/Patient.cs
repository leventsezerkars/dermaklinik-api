using System;

namespace DermaKlinik.API.Core.Entities
{
    public class Patient : AuditableEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
    }
} 