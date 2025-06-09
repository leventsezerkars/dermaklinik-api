using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Infrastructure.Data.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace DermaKlinik.API.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
            : base(options)
        {
            _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        }

        public DbSet<Menu> Menu { get; set; }
        public DbSet<MenuTranslation> MenuTranslation { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<CompanyInfo> CompanyInfo { get; set; }
        public DbSet<BlogCategory> BlogCategory { get; set; }
        public DbSet<BlogCategoryTranslation> BlogCategoryTranslation { get; set; }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<BlogTranslation> BlogTranslation { get; set; }
        public DbSet<GalleryImage> GalleryImage { get; set; }
        public DbSet<GalleryGroup> GalleryGroup { get; set; }
        public DbSet<GalleryImageGroupMap> GalleryImageGroupMap { get; set; }
        public DbSet<Log> Log { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            // Tüm Guid özellikleri için varsayılan olarak "uniqueidentifier" kullan
            configurationBuilder.Properties<Guid>().HaveColumnType("uniqueidentifier");

            // Foreign Key leri kaldır
            configurationBuilder.Conventions.Remove(typeof(ForeignKeyIndexConvention));

            configurationBuilder.Properties<string>().UseCollation("Latin1_General_CI_AI");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.UseCollation("Latin1_General_CI_AI");

            // AuditableEntity'den miras alan entity'ler için Creator ve Updater ilişkilerini tanımla
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(AuditableEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .HasOne("Creator")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Restrict);

                    modelBuilder.Entity(entityType.ClrType)
                        .HasOne("Updater")
                        .WithMany()
                        .HasForeignKey("UpdatedById")
                        .OnDelete(DeleteBehavior.Restrict);
                }
            }

            // Entity konfigürasyonları
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}