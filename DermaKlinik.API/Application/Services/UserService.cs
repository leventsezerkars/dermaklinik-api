using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DermaKlinik.API.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<IEnumerable<User>> GetActiveUsersAsync()
        {
            return await _userRepository.GetActiveUsersAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }

        public async Task<User> CreateUserAsync(User user, string password)
        {
            if (!await _userRepository.IsEmailUniqueAsync(user.Email))
            {
                throw new InvalidOperationException("Bu e-posta adresi zaten kullanılıyor.");
            }

            user.PasswordHash = _passwordHasher.HashPassword(user, password);
            user.CreatedAt = DateTime.UtcNow;
            user.IsActive = true;

            await _userRepository.AddAsync(user);
            return user;
        }

        public async Task UpdateUserAsync(User user)
        {
            var existingUser = await _userRepository.GetByIdAsync(user.Id);
            if (existingUser == null)
            {
                throw new KeyNotFoundException($"Kullanıcı bulunamadı: ID {user.Id}");
            }

            if (!await _userRepository.IsEmailUniqueAsync(user.Email, user.Id))
            {
                throw new InvalidOperationException("Bu e-posta adresi zaten kullanılıyor.");
            }

            user.UpdatedAt = DateTime.UtcNow;
            _userRepository.Update(user);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"Kullanıcı bulunamadı: ID {id}");
            }

            user.IsActive = false;
            user.UpdatedAt = DateTime.UtcNow;
            _userRepository.Update(user);
        }

        public async Task<bool> IsEmailUniqueAsync(string email, Guid? excludeUserId = null)
        {
            return await _userRepository.IsEmailUniqueAsync(email, excludeUserId);
        }

        public async Task<bool> ValidateUserAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null || !user.IsActive)
            {
                return false;
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result == PasswordVerificationResult.Success;
        }

        public async Task UpdateLastLoginAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                user.LastLoginAt = DateTime.UtcNow;
                _userRepository.Update(user);
            }
        }
    }
} 