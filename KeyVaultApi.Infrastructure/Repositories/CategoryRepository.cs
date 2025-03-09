using KeyVaultApi.Domain.Entities;
using KeyVaultApi.Infrastructure.Data;
using Dapper;
using KeyVaultApi.Application.Interfaces;
using System.Data;

namespace KeyVaultApi.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DatabaseContext _context;

        public CategoryRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<int> AddCategoryAsync(Category category)
        {
            if (category == null) throw new ArgumentNullException(nameof(category));

            var sql = @"INSERT INTO [logicvault].[dbo].[Category] (BusinessID, Name, Description, Active) 
                        VALUES (@BusinessID, @Name, @Description, @Active); 
                        SELECT CAST(SCOPE_IDENTITY() AS int);";

            try
            {
                using var connection = _context.CreateConnection();
                return await connection.QuerySingleAsync<int>(sql, new
                {
                    category.BusinessID,
                    category.Name,
                    category.Description,
                    category.Active
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar la categoría.", ex);
            }
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("ID inválido.", nameof(id));

            var sql = "DELETE FROM [logicvault].[dbo].[Category] WHERE CategoryId = @Id";

            try
            {
                using var connection = _context.CreateConnection();
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la categoría.", ex);
            }
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("ID inválido.", nameof(id));

            var sql = @"SELECT [CategoryID], [BusinessID], [Name], [Description], [Active] 
                        FROM [logicvault].[dbo].[Category] 
                        WHERE [CategoryID] = @Id";

            try
            {
                using var connection = _context.CreateConnection();
                var category = await connection.QueryFirstOrDefaultAsync<Category>(sql, new { Id = id });

                return category ?? throw new KeyNotFoundException("Categoría no encontrada.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la categoría.", ex);
            }
        }

        public async Task<List<Category>> GetAllCategorysAsync()
        {
            var sql = @"SELECT [CategoryID], [BusinessID], [Name], [Description], [Active] 
                FROM [logicvault].[dbo].[Category]";

            try
            {
                using var connection = _context.CreateConnection();
                var categories = await connection.QueryAsync<Category>(sql);
                return categories.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las categorías.", ex);
            }
        }



        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            if (category == null) throw new ArgumentNullException(nameof(category));
            if (category.CategoryId <= 0) throw new ArgumentException("ID inválido.", nameof(category.CategoryId));

            var sql = @"UPDATE [logicvault].[dbo].[Category] 
                        SET BusinessID = @BusinessID, Name = @Name, Description = @Description, Active = @Active 
                        WHERE CategoryId = @CategoryId";

            try
            {
                using var connection = _context.CreateConnection();
                var affectedRows = await connection.ExecuteAsync(sql, new
                {
                    category.BusinessID,
                    category.Name,
                    category.Description,
                    category.Active,
                    category.CategoryId
                });

                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la categoría.", ex);
            }
        }

    }
}
