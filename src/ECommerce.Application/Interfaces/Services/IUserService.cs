

using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(Guid id);
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(Guid id, User user);
        Task<bool> DeleteAsync(Guid id);
    }
}
