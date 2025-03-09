using KeyVaultApi.Application.Interfaces.Services;
using KeyVaultApi.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace KeyVaultApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            if (id <= 0)
                return BadRequest("El ID debe ser un número positivo.");

            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound("Categoría no encontrada.");

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddCategory([FromBody] Category category)
        {
            if (category == null)
                return BadRequest("La categoría no puede ser nula.");

            if (string.IsNullOrWhiteSpace(category.Name))
                return BadRequest("El nombre es obligatorio.");

            var newCategoryId = await _categoryService.AddCategoryAsync(category);
            return CreatedAtAction(nameof(GetCategoryById), new { id = newCategoryId }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category category)
        {
            if (id != category.CategoryId)
                return BadRequest("El ID en la ruta no coincide con el de la categoría.");

            if (category == null)
                return BadRequest("La categoría no puede ser nula.");

            if (string.IsNullOrWhiteSpace(category.Name))
                return BadRequest("El nombre es obligatorio.");

            var updatedCategory = await _categoryService.UpdateCategoryAsync(category);
            if (updatedCategory == null)
                return NotFound("No se pudo actualizar la categoría.");

            return Ok(updatedCategory);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (id <= 0)
                return BadRequest("El ID debe ser un número valido.");

            var deleted = await _categoryService.DeleteCategoryAsync(id);
            if (!deleted)
                return NotFound("No se pudo eliminar la categoría.");

            return NoContent();
        }
    }
}
