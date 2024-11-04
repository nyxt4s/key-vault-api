using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace KeyVaultApi.Infrastructure.Data
{
    public class DatabaseContext
    {
        private readonly string _connectionString;

        // Inyectar solo IConfiguration
        public DatabaseContext(IConfiguration configuration)
        {
            // Obtener la cadena de conexión desde la configuración
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
