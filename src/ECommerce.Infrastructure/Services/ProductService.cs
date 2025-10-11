

using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Domain.Entities;

namespace ECommerce.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            // ✅ Business rule: product must have a valid category
            var category = await _categoryRepository.GetByIdAsync(product.CategoryId);
            if (category == null)
                throw new Exception("Category not found.");

            await _productRepository.AddAsync(product);
            return product;
        }

        public async Task<Product> UpdateAsync(Guid id, Product product)
        {
            var existing = await _productRepository.GetByIdAsync(id);
            if (existing == null)
                throw new Exception("Product not found.");

            existing.Name = product.Name;
            existing.Price = product.Price;
            existing.Description = product.Description;
            existing.CategoryId = product.CategoryId;

            await _productRepository.UpdateAsync(existing);
            return existing;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await _productRepository.GetByIdAsync(id);
            if (existing == null)
                return false;

            await _productRepository.DeleteAsync(existing);
            return true;
        }
    }
}
