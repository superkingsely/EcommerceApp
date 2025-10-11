

using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Domain.Entities;

namespace ECommerce.Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<Category> CreateAsync(Category category)
        {
            // ✅ Business rule: Category name must be unique
            var existingCategories = await _categoryRepository.GetAllAsync();
            if (existingCategories.Any(c => c.Name.ToLower() == category.Name.ToLower()))
                throw new Exception("Category name already exists.");

            await _categoryRepository.AddAsync(category);
            return category;
        }

        public async Task<Category> UpdateAsync(Guid id, Category category)
        {
            var existing = await _categoryRepository.GetByIdAsync(id);
            if (existing == null)
                throw new Exception("Category not found.");

            existing.Name = category.Name;
            existing.Description = category.Description;

            await _categoryRepository.UpdateAsync(existing);
            return existing;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await _categoryRepository.GetByIdAsync(id);
            if (existing == null)
                return false;

            await _categoryRepository.DeleteAsync(existing);
            return true;
        }
    }
}
