using DermaKlinik.API.Core.Interfaces;
using DermaKlinik.API.Core.Models;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace DermaKlinik.API.Application.Services
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public interface IAuthService
    {
        Task<ApiResponse<string>> LoginAsync(LoginRequest request);
        object Logout();
        Task<ApiResponse<Core.Entities.User>> RegisterAsync(RegisterRequest request);
    }

    public class AuthService : IAuthService
    {
        private readonly IGenericRepository<Core.Entities.User> _userRepository;
        private readonly IPasswordHasher<Core.Entities.User> _passwordHasher;
        private readonly IJwtService _jwtService;

        public AuthService(
            IGenericRepository<Core.Entities.User> userRepository,
            IPasswordHasher<Core.Entities.User> passwordHasher,
            IJwtService jwtService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task<ApiResponse<string>> LoginAsync(LoginRequest request)
        {
            var user = (_userRepository.GetAll())
                .FirstOrDefault(u => u.Email == request.Email);

            if (user == null)
            {
                return ApiResponse<string>.ErrorResult("Kullanıcı bulunamadı");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (result != PasswordVerificationResult.Success)
            {
                return ApiResponse<string>.ErrorResult("Geçersiz şifre");
            }

            var token = _jwtService.GenerateToken(user);
            return ApiResponse<string>.SuccessResult(token);
        }

        public object Logout()
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<Core.Entities.User>> RegisterAsync(RegisterRequest request)
        {
            var existingUser = (_userRepository.GetAll())
                .FirstOrDefault(u => u.Email == request.Email);

            if (existingUser != null)
            {
                return ApiResponse<Core.Entities.User>.ErrorResult("Bu e-posta adresi zaten kullanılıyor");
            }

            var user = new Core.Entities.User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Role = "User",
                CreatedAt = DateTime.UtcNow
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
            await _userRepository.AddAsync(user);

            return ApiResponse<Core.Entities.User>.SuccessResult(user, "Kayıt başarılı", HttpStatusCode.Created);
        }
    }
}