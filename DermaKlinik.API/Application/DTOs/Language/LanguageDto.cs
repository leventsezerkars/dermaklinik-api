using DermaKlinik.API.Core.Entities;
using System;

namespace DermaKlinik.API.Application.DTOs.Language
{
    public class LanguageDto : AuditableDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsDefault { get; set; }
    }
}