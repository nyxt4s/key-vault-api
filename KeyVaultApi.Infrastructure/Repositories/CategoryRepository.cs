

using KeyVaultApi.Domain.Entities;

namespace KeyVaultApi.Application.Interfaces;

class CategoryRepository : ICategoryRepository
{
    public Task<int> AddCategoryAsync(Category Category)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteCategoryAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Category> GetCategoryByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Category>> GetCategorysAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateCategoryAsync(Category Category)
    {
        throw new NotImplementedException();
    }
}
