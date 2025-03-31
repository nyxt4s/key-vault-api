using KeyVaultApi.Application.DTOs;
using KeyVaultApi.Application.Interfaces.Services;
using KeyVaultApi.Application.Services;
using KeyVaultApi.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace KeyVaultApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _ProductService;

        public ProductController(IProductService productService)
        {
            _ProductService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetAllProductsd(int businessId)
        {
            var products = await _ProductService.GetAllProductsAsync(businessId);
            return Ok(products);
        }
    }
}
