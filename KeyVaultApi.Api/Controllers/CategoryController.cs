using KeyVaultApi.Application.Interfaces;
using KeyVaultApi.Domain.Entities;
using KeyVaultApi.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace KeyVaultApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }


        [HttpGet]
        public async Task<ActionResult<List<Brand>>> GetAllBrands()
        {
            var categorys = await _categoryRepository.GetCategorysAsync();
            return Ok(categorys);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetBrandById(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddBrand(Category category)
        {
            var newCategoryId = await _categoryRepository.AddCategoryAsync(category);
            return CreatedAtAction(nameof(GetBrandById), new { id = newCategoryId }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBrand(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest();
            }

            var updated = await _categoryRepository.UpdateCategoryAsync(category);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var deleted = await _categoryRepository.DeleteCategoryAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
