using System.ComponentModel.DataAnnotations;

namespace DermaKlinik.API.Core.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
    public abstract class BaseDto
    {
        public Guid? Id { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }
}