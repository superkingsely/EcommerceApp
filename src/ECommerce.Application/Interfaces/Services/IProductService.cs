
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(Guid id);
        Task<Product> CreateAsync(Product product);
        Task<Product> UpdateAsync(Guid id, Product product);
        Task<bool> DeleteAsync(Guid id);
    }
}
