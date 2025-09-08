using DermaKlinik.API.Core.Models;
using DermaKlinik.API.Presentation.Middleware;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DermaKlinik.API.Core.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddDualAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // JWT Settings
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
            if (jwtSettings == null)
            {
                throw new InvalidOperationException("JwtSettings configuration is missing.");
            }

            // API Key Settings
            services.Configure<ApiKeySettings>(configuration.GetSection("ApiKeySettings"));
            var apiKeySettings = configuration.GetSection("ApiKeySettings").Get<ApiKeySettings>();
            if (apiKeySettings == null)
            {
                throw new InvalidOperationException("ApiKeySettings configuration is missing.");
            }

            // JWT Key için hem JwtSettings hem de Jwt bölümünü kontrol et
            var jwtKey = configuration["Jwt:Key"] ?? jwtSettings.SecretKey;
            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new InvalidOperationException("JWT Key is not configured in either JwtSettings.SecretKey or Jwt:Key");
            }

            var key = Encoding.ASCII.GetBytes(jwtKey);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtOrApiKey";
                options.DefaultChallengeScheme = "JwtOrApiKey";
            })
            .AddJwtBearer("Jwt", options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = configuration["Jwt:Issuer"] ?? jwtSettings.Issuer,
                    ValidAudience = configuration["Jwt:Audience"] ?? jwtSettings.Audience,
                    ClockSkew = TimeSpan.Zero
                };
            })
            .AddScheme<ApiKeyAuthenticationSchemeOptions, ApiKeyAuthenticationHandler>("ApiKey", options =>
            {
                options.ApiKey = apiKeySettings.SecretKey;
                options.HeaderName = apiKeySettings.HeaderName;
            })
            .AddPolicyScheme("JwtOrApiKey", "JWT or API Key", options =>
            {
                options.ForwardDefaultSelector = context =>
                {
                    string authorization = context.Request.Headers.Authorization;
                    if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith("Bearer "))
                    {
                        return "Jwt";
                    }

                    string apiKey = context.Request.Headers[apiKeySettings.HeaderName];
                    if (!string.IsNullOrEmpty(apiKey))
                    {
                        return "ApiKey";
                    }

                    return "Jwt"; // Default to JWT
                };
            });

            return services;
        }
    }
}
