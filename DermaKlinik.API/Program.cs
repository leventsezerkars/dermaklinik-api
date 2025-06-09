using DermaKlinik.API.Application.Common.Behaviors;
using DermaKlinik.API.Application.Services;
using DermaKlinik.API.Application.Services.Menu;
using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Core.Interfaces;
using DermaKlinik.API.Core.Models;
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

var builder = WebApplication.CreateBuilder(args);

// Serilog yapılandırması
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

// DbContext Configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure();
    }));

// JWT Configuration
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
if (jwtSettings == null)
{
    throw new InvalidOperationException("JwtSettings configuration is missing.");
}
var key = Encoding.ASCII.GetBytes(jwtSettings.SecretKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        ClockSkew = TimeSpan.Zero
    };
});

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

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<ILogRepository, LogRepository>();
builder.Services.AddScoped<ILogService, LogService>();

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
                             .WithHeaders("Authorization", "Content-Type", "Accept", "X-Requested-With")
                             .WithExposedHeaders("X-Pagination")
                             .SetIsOriginAllowed(origin => true)
                             .AllowCredentials();
            });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Global Exception Middleware
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseCors("DefaultPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
