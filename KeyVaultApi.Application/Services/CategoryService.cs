using KeyVaultApi.Application.Interfaces;
using KeyVaultApi.Application.Interfaces.Services;
using KeyVaultApi.Domain.Entities;

namespace KeyVaultApi.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.Name))
                throw new ArgumentException("El nombre de la categoría es obligatorio.");

            if (string.IsNullOrWhiteSpace(category.Description))
                throw new ArgumentException("La descripción de la categoría es obligatoria.");

            category.Active = true; // Por defecto, la categoría está activa

            int newCategoryId = await _categoryRepository.AddCategoryAsync(category);
            return await _categoryRepository.GetCategoryByIdAsync(newCategoryId);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
                throw new KeyNotFoundException("La categoría no existe.");

            return await _categoryRepository.DeleteCategoryAsync(id);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllCategorysAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
                throw new KeyNotFoundException("La categoría no existe.");

            return category;
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            if (category.CategoryId <= 0)
                throw new ArgumentException("ID de categoría inválido.");

            if (string.IsNullOrWhiteSpace(category.Name))
                throw new ArgumentException("El nombre de la categoría es obligatorio.");

            if (string.IsNullOrWhiteSpace(category.Description))
                throw new ArgumentException("La descripción de la categoría es obligatoria.");

            var existingCategory = await _categoryRepository.GetCategoryByIdAsync(category.CategoryId);
            if (existingCategory == null)
                throw new KeyNotFoundException("La categoría no existe.");

            bool updated = await _categoryRepository.UpdateCategoryAsync(category);
            if (!updated)
                throw new InvalidOperationException("No se pudo actualizar la categoría.");

            return await _categoryRepository.GetCategoryByIdAsync(category.CategoryId);
        }
    }
}
