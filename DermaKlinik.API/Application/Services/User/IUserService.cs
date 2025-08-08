using DermaKlinik.API.Application.DTOs.User;

namespace DermaKlinik.API.Application.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<IEnumerable<UserDto>> GetActiveUsersAsync();
        Task<UserDto> GetByIdAsync(Guid id);
        Task<UserDto> GetByEmailAsync(string email);
        Task<UserDto> GetByUsernameAsync(string username);
        Task<UserDto> CreateAsync(CreateUserDto createUserDto);
        Task<UserDto> UpdateAsync(Guid id, UpdateUserDto updateUserDto);
        Task DeleteAsync(Guid id);
        Task<bool> IsUsernameUniqueAsync(string username);
        Task<bool> IsEmailUniqueAsync(string email);
        Task ChangePasswordAsync(ChangePasswordDto changePasswordDto);
        Task UpdateLastLoginAsync(Guid userId);
        Task<UserDto> ValidateUserAsync(string username, string password);
    }
}