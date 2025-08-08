using System.ComponentModel.DataAnnotations;

namespace DermaKlinik.API.Core.Entities
{
    public class CompanyInfo : AuditableEntity
    {
        [StringLength(5000)]
        public string LogoUrl { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        [StringLength(100)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(255)]
        public string Facebook { get; set; }

        [StringLength(255)]
        public string Twitter { get; set; }

        [StringLength(255)]
        public string Instagram { get; set; }

        [StringLength(255)]
        public string LinkedIn { get; set; }

        [StringLength(1000)]
        public string GoogleMapsUrl { get; set; }

        [StringLength(1000)]
        public string GoogleAnalyticsCode { get; set; }

        [StringLength(255)]
        public string MetaTitle { get; set; }

        [StringLength(1000)]
        public string MetaDescription { get; set; }

        [StringLength(255)]
        public string MetaKeywords { get; set; }

        [StringLength(255)]
        public string WorkingHours { get; set; }
    }
}