using DermaKlinik.API.Core.Entities;

namespace DermaKlinik.API.Application.DTOs.CompanyInfo
{
    public class CompanyInfoDto : AuditableEntity
    {
        public string LogoUrl { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string LinkedIn { get; set; }
        public string GoogleMapsUrl { get; set; }
        public string GoogleAnalyticsCode { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
        public string WorkingHours { get; set; }
    }
}