using DermaKlinik.API.Application.DTOs.User;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DermaKlinik.API.Application.Services
{
    public interface IJwtService
    {
        string GenerateToken(UserDto user);
    }

    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(UserDto user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            var key = _configuration["Jwt:Key"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var expiresInDaysStr = _configuration["Jwt:ExpiresInDays"] ?? "7";

            if (string.IsNullOrEmpty(key))
                throw new InvalidOperationException("JWT Key is not configured");

            if (string.IsNullOrEmpty(issuer))
                throw new InvalidOperationException("JWT Issuer is not configured");

            if (string.IsNullOrEmpty(audience))
                throw new InvalidOperationException("JWT Audience is not configured");

            if (!int.TryParse(expiresInDaysStr, out int expiresInDays))
                expiresInDays = 7;

            var keyBytes = Encoding.ASCII.GetBytes(key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Username)
                }),
                Issuer = issuer,
                Audience = audience,
                Expires = DateTime.UtcNow.AddDays(expiresInDays),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}