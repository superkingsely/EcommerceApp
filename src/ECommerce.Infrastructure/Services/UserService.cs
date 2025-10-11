

using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Domain.Entities;

namespace ECommerce.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User> CreateAsync(User user)
        {
            // ✅ Business rule: Email must be unique
            var existingUsers = await _userRepository.GetAllAsync();
            if (existingUsers.Any(u => u.Email.ToLower() == user.Email.ToLower()))
                throw new Exception("Email already exists.");

            user.CreatedAt = DateTime.UtcNow;

            await _userRepository.AddAsync(user);
            return user;
        }

        public async Task<User> UpdateAsync(Guid id, User user)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
                throw new Exception("User not found.");

            existingUser.FullName = user.FullName;
            existingUser.Email = user.Email;
            existingUser.PasswordHash = user.PasswordHash;

            await _userRepository.UpdateAsync(existingUser);
            return existingUser;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await _userRepository.GetByIdAsync(id);
            if (existing == null)
                return false;

            await _userRepository.DeleteAsync(existing);
            return true;
        }
    }
}
