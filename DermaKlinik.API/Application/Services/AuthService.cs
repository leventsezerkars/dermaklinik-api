using DermaKlinik.API.Application.DTOs.User;
using DermaKlinik.API.Core.Interfaces;
using DermaKlinik.API.Core.Models;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace DermaKlinik.API.Application.Services
{


    public interface IAuthService
    {
        Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginDto request);
        Task<ApiResponse<bool>> LogoutAsync();
        Task<ApiResponse<UserDto>> RegisterAsync(CreateUserDto request);
    }

    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public AuthService(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        public async Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginDto request)
        {
            var user = await _userService.ValidateUserAsync(request.Username, request.Password);
            
            if (user == null)
            {
                return ApiResponse<LoginResponseDto>.ErrorResult("Geçersiz kullanıcı adı veya şifre");
            }

            if (!user.IsActive)
            {
                return ApiResponse<LoginResponseDto>.ErrorResult("Kullanıcı hesabı aktif değil");
            }

            var token = _jwtService.GenerateToken(user);
            
            // Son giriş tarihini güncelle
            await _userService.UpdateLastLoginAsync(user.Id);

            var response = new LoginResponseDto
            {
                Token = token,
                User = user
            };

            return ApiResponse<LoginResponseDto>.SuccessResult(response, "Giriş başarılı");
        }

        public async Task<ApiResponse<bool>> LogoutAsync()
        {
            // JWT token'ları stateless olduğu için client tarafında silinmesi yeterli
            // Burada ek işlemler yapılabilir (örn: token blacklist)
            return ApiResponse<bool>.SuccessResult(true, "Çıkış başarılı");
        }

        public async Task<ApiResponse<UserDto>> RegisterAsync(CreateUserDto request)
        {
            try
            {
                var user = await _userService.CreateAsync(request);
                return ApiResponse<UserDto>.SuccessResult(user, "Kayıt başarılı", HttpStatusCode.Created);
            }
            catch (InvalidOperationException ex)
            {
                return ApiResponse<UserDto>.ErrorResult(ex.Message);
            }
        }
    }
}