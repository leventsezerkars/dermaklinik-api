namespace DermaKlinik.API.Core.Entities
{
    public class BlogCategory : AuditableEntity
    {
        public virtual ICollection<Blog> Blogs { get; set; }
        public virtual ICollection<BlogCategoryTranslation> Translations { get; set; }
    }
}