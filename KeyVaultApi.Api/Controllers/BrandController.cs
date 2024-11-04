using Microsoft.AspNetCore.Mvc;
using KeyVaultApi.Application.Interfaces;
using KeyVaultApi.Domain.Entities;


namespace KeyVaultApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandRepository _brandRepository;

        public BrandController(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Brand>>> GetAllBrands()
        {
            var brands = await _brandRepository.GetAllBrandsAsync();
            return Ok(brands);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetBrandById(int id)
        {
            var brand = await _brandRepository.GetBrandByIdAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            return Ok(brand);
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddBrand(Brand brand)
        {
            var newBrandId = await _brandRepository.AddBrandAsync(brand);
            return CreatedAtAction(nameof(GetBrandById), new { id = newBrandId }, brand);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBrand(int id, Brand brand)
        {
            if (id != brand.BrandId)
            {
                return BadRequest();
            }

            var updated = await _brandRepository.UpdateBrandAsync(brand);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var deleted = await _brandRepository.DeleteBrandAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
