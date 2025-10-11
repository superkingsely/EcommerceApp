

using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order?> GetByIdAsync(Guid id);
        Task<Order> CreateAsync(Order order);
        Task<Order> UpdateAsync(Guid id, Order order);
        Task<bool> DeleteAsync(Guid id);
    }
}
