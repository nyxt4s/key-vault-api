using KeyVaultApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyVaultApi.Application.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategorysAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task<int> AddCategoryAsync(Category Category);
        Task<bool> UpdateCategoryAsync(Category Category);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
