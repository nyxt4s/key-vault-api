using Dapper;
using KeyVaultApi.Application.Interfaces;
using KeyVaultApi.Domain.Entities;
using KeyVaultApi.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyVaultApi.Infrastructure.Repositories
{
    public class BusinessRepository : IBusinessRepository
    {
        private readonly DatabaseContext _context;
        public BusinessRepository(DatabaseContext databaseContext)
        {
            _context = databaseContext;
        }

        public async Task<Business> AddBusinessAsync(Business business)
        {
            // Consulta SQL para insertar un nuevo negocio en la base de datos y obtener el ID generado
            var sql = "INSERT INTO [logicvault].[dbo].[Business] ([Username], [Name], [Email], [Address], [Password], [Phone], [Active]) " +
                      "VALUES (@Username, @Name, @Email, @Address, @Password, @Phone, @Active); " +
                      "SELECT CAST(SCOPE_IDENTITY() AS int);";

            // Crear una conexión a la base de datos
            using (var connection = _context.CreateConnection())
            {
                // Ejecutar la consulta SQL y capturar el ID del nuevo negocio
                var newBusinessId = await connection.QuerySingleAsync<int>(sql, new
                {
                    UserName = business.UserName,
                    Name = business.Name,
                    Email = business.Email,
                    Password = business.Password,
                    Phone = business.Phone,
                    Address = business.Address,
                    Active = business.Active
                });

                // Asignar el ID generado al objeto de negocio
                business.BusinessID = newBusinessId;

                // Retornar el objeto completo
                return business;
            }
        }



        public async Task<bool> DeleteBusinessAsync(int id)
        {
            var sql = "DELETE FROM [logicvault].[dbo].[Business] WHERE BusinessID = @Id";
            using (var connection = _context.CreateConnection())
            {
                // Ejecutar la consulta y retornar el número de filas afectadas
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
                return affectedRows > 0; // Retornar true si se eliminó al menos una fila
            }
        }

        public async Task<List<Business>> GetAllBusinessAsync()
        {
            var sql = "SELECT [BusinessID] ,[Name],[Email], [Address], [Password], [Phone], [Active] FROM [logicvault].[dbo].[Business]";
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync(sql);

                // Crear la lista de negocios y asignar propiedades manualmente
                List<Business> business = new List<Business>();

                foreach (var row in result)
                {
                    Business busines = new Business
                    {
                        BusinessID = row.BusinessID, // Asignación explícita de cada propiedad
                        Name = row.Name,
                        Email = row.Email,
                        Address = row.Adress,
                        Password = row.Password,
                        Phone = row.Phone,
                        Active = row.Active,

                    };
                    business.Add(busines);
                }
                return business;
            }
        }

        public async Task<Business> GetBusinessByIdAsync(int id)
        {
            var sql = "SELECT [BusinessID] ,[Name],[Email], [Address], [Phone], [Active] FROM [logicvault].[dbo].[Business] WHERE BusinessID = @Id";
            using (var connection = _context.CreateConnection())
            {
                // Usar QueryFirstOrDefaultAsync para obtener un solo resultado
                var result = await connection.QueryFirstOrDefaultAsync<Business>(sql, new { Id = id });

                if (result == null)
                {
                    throw new Exception("Negocio no encontrado");
                }
                else
                {
                    return result;
                }
            }
        }
        public async Task<Business> UpdateBusinessAsync(Business business)
        {
            var updateSql = @"
                            UPDATE [logicvault].[dbo].[Business]
                            SET 
                                Name = @Name,
                                UserName = @UserName,
                                Password = @Password,
                                Address = @Address,
                                Phone = @Phone,
                                Email = @Email,
                                Active = @Active
                            WHERE BusinessID = @BusinessID";

            var selectSql = "SELECT * FROM [logicvault].[dbo].[Business] WHERE BusinessID = @BusinessID";

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    // Ejecutar la actualización de los campos permitidos
                    var affectedRows = await connection.ExecuteAsync(updateSql, new
                    {
                        business.Name,
                        business.UserName,
                        business.Password,
                        business.Address,
                        business.Phone,
                        business.Email,
                        business.Active,
                        business.BusinessID
                    });

                    // Si no se actualizó ninguna fila, lanzar una excepción
                    if (affectedRows == 0)
                    {
                        throw new InvalidOperationException("No se encontró el negocio o no se pudo actualizar.");
                    }

                    // Retornar el negocio actualizado desde la base de datos
                    var updatedBusiness = await connection.QuerySingleAsync<Business>(selectSql, new { business.BusinessID });
                    return updatedBusiness;
                }
            }
            catch (SqlException sqlEx)
            {
                // Manejo específico de errores de SQL, puedes loguear o manejar estos casos de forma diferente
                throw new Exception("Error al actualizar el negocio en la base de datos.", sqlEx);
            }
            catch (Exception ex)
            {
                // Manejo genérico de otros errores
                throw new Exception("Ocurrió un error inesperado al intentar actualizar el negocio.", ex);
            }
        }
    }
}
