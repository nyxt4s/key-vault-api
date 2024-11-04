using Microsoft.Extensions.Configuration;
using KeyVaultApi.Infrastructure.Data;
using KeyVaultApi.Infrastructure.Repositories;
using KeyVaultApi.Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Configura la cadena de conexión en el archivo appsettings.json
builder.Configuration.AddJsonFile("appsettings.json");

// Agregar servicios al contenedor.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<DatabaseContext>(); // Cambiado para solo inyectar IConfiguration
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddControllers();

var app = builder.Build();

// Configura el pipeline de solicitud
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
