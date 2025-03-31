using Dapper;
using KeyVaultApi.Application.DTOs;
using KeyVaultApi.Application.Interfaces;
using KeyVaultApi.Domain.Entities;
using KeyVaultApi.Infrastructure.Data;

public class ProductRepository : IProductRepository
{
    private readonly DatabaseContext _context;

    public ProductRepository(DatabaseContext context)
    {
        _context = context;
    }

    public Task<int> CreateProductAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteProductAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync(int businessId)
    {
        var query = @"
                SELECT 
                    p.ProductID,
                    p.Name AS ProductName,
                    p.Description,
                    p.Price,
                    p.Active, 
                    p.CreationDate,
                    p.UpdateDate,
                    b.BusinessID,
                    b.Name AS BusinessName,
                    br.BrandID,
                    br.Name AS BrandName,
                    c.CategoryID,
                    c.Name AS CategoryName
                FROM Product p
                INNER JOIN Business b ON b.BusinessID = p.BusinessID
                INNER JOIN Brand br ON br.BrandID = p.BrandID
                INNER JOIN Category c ON c.CategoryID = p.CategoryID
                WHERE b.BusinessID = @BusinessID";

        using var connection = _context.CreateConnection();
        var result = await connection.QueryAsync(query, new { BusinessID = businessId });

        List<ProductDto> products = new List<ProductDto>();

        foreach (var row in result)
        {
            ProductDto product = new ProductDto
            {
                ProductId = row.ProductID,
                Name = row.ProductName,
                Description = row.Description,
                Price = row.Price,
                Active = row.Active,
                CreationDate = row.CreationDate,
                UpdateDate = row.UpdateDate,
                Business = new BusinessDto
                {
                    BusinessID = row.BusinessID,
                    Name = row.BusinessName
                },
                Brand = new BrandDto
                {
                    BrandId = row.BrandID,
                    Name = row.BrandName
                },
                Category = new CategoryDto
                {
                    CategoryId = row.CategoryID,
                    Name = row.CategoryName
                }
            };

            products.Add(product);
        }

        return products;
    }



    public Task<Product> GetProductByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateProductAsync(Product product)
    {
        throw new NotImplementedException();
    }
}
