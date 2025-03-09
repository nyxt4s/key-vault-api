using KeyVaultApi.Domain.Entities;


namespace KeyVaultApi.Application.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategorysAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task<int> AddCategoryAsync(Category Category);
        Task<bool> UpdateCategoryAsync(Category Category);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
