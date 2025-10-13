using DermaKlinik.API.Application.Common.Behaviors;
using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Application.Services.Menu;
using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Core.Interfaces;
using DermaKlinik.API.Core.Models;
using DermaKlinik.API.Core.Extensions;
using DermaKlinik.API.Infrastructure.Data;
using DermaKlinik.API.Infrastructure.Repositories;
using DermaKlinik.API.Infrastructure.UnitOfWork;
using DermaKlinik.API.Presentation.Middleware;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using Log = Serilog.Log;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using DermaKlinik.API.Application.Services.Language;
using DermaKlinik.API.Infrastructure.Data.Interceptors;
using DermaKlinik.API.Application.Services.Email;

var builder = WebApplication.CreateBuilder(args);

// Configuration yükleme - Environment-specific dosyalar otomatik yüklenir
// appsettings.json -> appsettings.{Environment}.json sırasıyla yüklenir
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Serilog yapılandırması
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllers(options =>
{
    // Model validation filter'ı ekle
    options.Filters.Add<DermaKlinik.API.Presentation.Middleware.ModelValidationFilter>();
    // ASP.NET Core'un varsayılan model validation'ını devre dışı bırak
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
})
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    })
    .ConfigureApiBehaviorOptions(options =>
    {
        // ASP.NET Core'un varsayılan Problem Details yanıtını devre dışı bırak
        options.SuppressMapClientErrors = true;
        options.SuppressModelStateInvalidFilter = true;
    });

// DbContext Configuration
builder.Services.AddScoped<AuditableEntitySaveChangesInterceptor>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure();
    }));

// Dual Authentication Configuration (JWT + API Key)
builder.Services.AddDualAuthentication(builder.Configuration);

// Repository ve Service kayıtları
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// AutoMapper Configuration
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// HttpContextAccessor
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddScoped<IBlogTranslationRepository, BlogTranslationRepository>();
builder.Services.AddScoped<IBlogCategoryRepository, BlogCategoryRepository>();
builder.Services.AddScoped<IBlogCategoryTranslationRepository, BlogCategoryTranslationRepository>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IMenuTranslationRepository, MenuTranslationRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILanguageRepository, LanguageRepository>();
builder.Services.AddScoped<ICompanyInfoRepository, CompanyInfoRepository>();
builder.Services.AddScoped<IGalleryImageRepository, GalleryImageRepository>();
builder.Services.AddScoped<IGalleryGroupRepository, GalleryGroupRepository>();
builder.Services.AddScoped<IGalleryImageGroupMapRepository, GalleryImageGroupMapRepository>();

// Email Service
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<ICompanyInfoService, CompanyInfoService>();
builder.Services.AddScoped<ILogRepository, LogRepository>();
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<ILanguageService, LanguageService>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IBlogCategoryService, BlogCategoryService>();
builder.Services.AddScoped<IGalleryImageService, GalleryImageService>();
builder.Services.AddScoped<IGalleryGroupService, GalleryGroupService>();
builder.Services.AddScoped<IGalleryImageGroupMapService, GalleryImageGroupMapService>();
builder.Services.AddScoped<IFileUploadService, FileUploadService>();
builder.Services.AddScoped<IImageResizeService, ImageResizeService>();

// MediatR Configuration
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// FluentValidation Configuration
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DermaKlinik API", Version = "v1" });

    // JWT Authentication için Swagger yapılandırması
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    // API Key Authentication için Swagger yapılandırması
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "API Key Authentication. Example: \"X-API-Key: {your-api-key}\"",
        Name = "X-API-Key",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        },
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                }
            },
            Array.Empty<string>()
        }
    });
});

// CORS politikası
builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultPolicy",
            policyBuilder =>
            {
                // Corrected the usage of builder.Configuration.GetSection
                var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
                policyBuilder.WithOrigins(allowedOrigins)
                             .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
                             .WithHeaders("Authorization", "Content-Type", "Accept", "X-Requested-With", "X-API-Key")
                             .WithExposedHeaders("X-Pagination")
                             .SetIsOriginAllowed(origin => true)
                             .AllowCredentials();
            });
});

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.
// Swagger'ı tüm ortamlarda kullanılabilir hale getir
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "DermaKlinik API v1");
    c.RoutePrefix = "swagger"; // Swagger UI'ı /swagger endpoint'inde erişilebilir yapar
});

// Global Exception Middleware - En üstte olmalı
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

// Static files serving
app.UseStaticFiles();

// CORS
app.UseCors("DefaultPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
