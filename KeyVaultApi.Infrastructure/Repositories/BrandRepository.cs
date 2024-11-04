using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using KeyVaultApi.Application.Interfaces;
using KeyVaultApi.Domain.Entities;
using KeyVaultApi.Infrastructure.Data;

namespace KeyVaultApi.Infrastructure.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly DatabaseContext _context;

        public BrandRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Brand>> GetAllBrandsAsync()
        {
            var sql = "SELECT BrandId, Name FROM [logicvault].[dbo].[Brand]";
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync(sql);

                // Crear la lista de Brand y asignar propiedades manualmente
                List<Brand> brands = new List<Brand>();

                foreach (var row in result)
                {
                    Brand brand = new Brand
                    {
                        BrandId = row.BrandId, // Asignación explícita de cada propiedad
                        Name = row.Name
                    };
                    brands.Add(brand);
                }

                return brands;
            }
        }

        public async Task<Brand> GetBrandByIdAsync(int id)
        {
            var sql = "SELECT BrandId, Name FROM [logicvault].[dbo].[Brand] WHERE BrandId = @Id"; 
            using (var connection = _context.CreateConnection())
            {
                // Usar QueryFirstOrDefaultAsync para obtener un solo resultado
                var result = await connection.QueryFirstOrDefaultAsync<Brand>(sql, new { Id = id });

                if (result == null)
                {
                    throw new Exception("marca no encontrada");
                }else
                {
                   return result;
                }
            }
        }

        public async Task<int> AddBrandAsync(Brand brand)
        {
            var sql = "INSERT INTO [logicvault].[dbo].[Brand] (Name, Active) VALUES (@Name, @Active); SELECT CAST(SCOPE_IDENTITY() AS int);";
            using (var connection = _context.CreateConnection())
            {
                // Ejecutar la consulta y retornar el ID de la nueva marca
                var newBrandId = await connection.QuerySingleAsync<int>(sql, new { Name = brand.Name, Active = brand.Active });
                return newBrandId; // Retornar el ID de la nueva marca
            }
        }

        public async Task<bool> UpdateBrandAsync(Brand brand)
        {
            var sql = "UPDATE [logicvault].[dbo].[Brand] SET Name = @Name WHERE BrandId = @BrandId";
            using (var connection = _context.CreateConnection())
            {
                // Ejecutar la consulta y retornar el número de filas afectadas
                var affectedRows = await connection.ExecuteAsync(sql, new { Name = brand.Name, BrandId = brand.BrandId });
                return affectedRows > 0; // Retornar true si se actualizó al menos una fila
            }
        }

        public async Task<bool> DeleteBrandAsync(int id)
        {
            var sql = "DELETE FROM [logicvault].[dbo].[Brand] WHERE BrandId = @Id";
            using (var connection = _context.CreateConnection())
            {
                // Ejecutar la consulta y retornar el número de filas afectadas
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
                return affectedRows > 0; // Retornar true si se eliminó al menos una fila
            }
        }



    }
}
