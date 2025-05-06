using System.ComponentModel.DataAnnotations;

namespace DermaKlinik.API.Core.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
} 