using AutoMapper;
using DermaKlinik.API.Application.DTOs.User;
using DermaKlinik.API.Core.Interfaces;
using DermaKlinik.API.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace DermaKlinik.API.Application.Services
{
    public class UserService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, IUserRepository userRepository) : IUserService
    {
        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = userRepository.GetAll();
            return mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var user = await userRepository.GetByIdAsync(id);
            return mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetByUsernameAsync(string username)
        {
            var user = await userRepository.Query()
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with username {username} not found.");
            }

            return mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> CreateAsync(CreateUserDto createUserDto)
        {
            if (!await IsUsernameUniqueAsync(createUserDto.Username))
            {
                throw new InvalidOperationException($"Username {createUserDto.Username} is already in use.");
            }

            if (!await IsEmailUniqueAsync(createUserDto.Email))
            {
                throw new InvalidOperationException($"Email {createUserDto.Email} is already in use.");
            }

            var user = mapper.Map<Core.Entities.User>(createUserDto);
            user.PasswordHash = HashPassword(createUserDto.Password);
            user.CreatedAt = DateTime.UtcNow;

            await userRepository.AddAsync(user);
            await unitOfWork.CompleteAsync();

            return mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UpdateAsync(Guid id, UpdateUserDto updateUserDto)
        {
            var user = await userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return null;
            }

            if (user.Username != updateUserDto.Username && !await IsUsernameUniqueAsync(updateUserDto.Username))
            {
                throw new InvalidOperationException($"Username {updateUserDto.Username} is already in use.");
            }

            if (user.Email != updateUserDto.Email && !await IsEmailUniqueAsync(updateUserDto.Email))
            {
                throw new InvalidOperationException($"Email {updateUserDto.Email} is already in use.");
            }

            mapper.Map(updateUserDto, user);
            user.UpdatedAt = DateTime.UtcNow;

            userRepository.Update(user);
            await unitOfWork.CompleteAsync();

            return mapper.Map<UserDto>(user);
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await userRepository.GetByIdAsync(id);
            if (user != null)
            {
                userRepository.SoftDelete(user);
                await unitOfWork.CompleteAsync();
            }
        }

        public async Task<bool> IsUsernameUniqueAsync(string username)
        {
            return !await userRepository.Query()
                .AnyAsync(u => u.Username == username);
        }

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            return !await userRepository.Query()
                .AnyAsync(u => u.Email == email);
        }



        public async Task ChangePasswordAsync(ChangePasswordDto changePasswordDto)
        {
            var user = await userRepository.Query()
                .FirstOrDefaultAsync(u => u.Id == changePasswordDto.Id);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {changePasswordDto.Id} not found.");
            }

            if (!VerifyPassword(changePasswordDto.CurrentPassword, user.PasswordHash))
            {
                throw new InvalidOperationException("Current password is incorrect.");
            }

            if (changePasswordDto.NewPassword != changePasswordDto.ConfirmPassword)
            {
                throw new InvalidOperationException("New password and confirmation password do not match.");
            }

            user.PasswordHash = HashPassword(changePasswordDto.NewPassword);
            user.UpdatedAt = DateTime.UtcNow;

            userRepository.Update(user);
            await unitOfWork.CompleteAsync();
        }

        public async Task UpdateLastLoginAsync(Guid userId)
        {
            var user = await userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                user.LastLoginDate = DateTime.UtcNow;
                userRepository.Update(user);
                await unitOfWork.CompleteAsync();
            }
        }

        public async Task<UserDto> ValidateUserAsync(string username, string password)
        {
            var user = await userRepository.Query()
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null || !VerifyPassword(password, user.PasswordHash))
            {
                return null;
            }

            return mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> GetActiveUsersAsync()
        {
            var users = await userRepository.Query()
                .Where(u => u.IsActive)
                .ToListAsync();
            return mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetByEmailAsync(string email)
        {
            var user = await userRepository.Query()
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with email {email} not found.");
            }

            return mapper.Map<UserDto>(user);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }


    }
}