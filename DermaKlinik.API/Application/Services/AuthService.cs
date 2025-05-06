using System.Security.Claims;
using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Core.Interfaces;
using DermaKlinik.API.Core.Models;
using DermaKlinik.API.Core.Models.Auth;
using Microsoft.AspNetCore.Identity;

namespace DermaKlinik.API.Application.Services
{
    public interface IAuthService
    {
        Task<ApiResponse<string>> LoginAsync(LoginRequest request);
        Task<ApiResponse<User>> RegisterAsync(RegisterRequest request);
        ApiResponse<bool> Logout();
    }

    public class AuthService : IAuthService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthService(
            IGenericRepository<User> userRepository,
            IJwtService jwtService,
            IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _passwordHasher = passwordHasher;
        }

        public async Task<ApiResponse<string>> LoginAsync(LoginRequest request)
        {
            var user = (await _userRepository.GetAllAsync())
                .FirstOrDefault(u => u.Email == request.Email && u.IsActive);

            if (user == null)
                return ApiResponse<string>.ErrorResult("Geçersiz e-posta veya şifre", 401);

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (result != PasswordVerificationResult.Success)
                return ApiResponse<string>.ErrorResult("Geçersiz e-posta veya şifre", 401);

            user.LastLoginAt = DateTime.UtcNow;
            _userRepository.Update(user);

            var token = _jwtService.GenerateToken(user);
            return ApiResponse<string>.SuccessResult(token, "Giriş başarılı");
        }

        public async Task<ApiResponse<User>> RegisterAsync(RegisterRequest request)
        {
            var existingUser = (await _userRepository.GetAllAsync())
                .FirstOrDefault(u => u.Email == request.Email);

            if (existingUser != null)
                return ApiResponse<User>.ErrorResult("Bu e-posta adresi zaten kullanılıyor");

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Role = "User",
                IsActive = true
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
            await _userRepository.AddAsync(user);

            return ApiResponse<User>.SuccessResult(user, "Kayıt başarılı", 201);
        }

        public ApiResponse<bool> Logout()
        {
            // JWT token'ları stateless olduğu için sunucu tarafında bir işlem yapmamıza gerek yok
            // Client tarafında token'ı silmek yeterli
            return ApiResponse<bool>.SuccessResult(true, "Çıkış başarılı");
        }
    }
} 