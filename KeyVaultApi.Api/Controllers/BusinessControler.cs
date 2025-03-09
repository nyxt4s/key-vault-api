using KeyVaultApi.Application.Interfaces.Services;
using KeyVaultApi.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeyVaultApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessController : ControllerBase
    {
        private readonly IBusinessService _businessService;

        public BusinessController(IBusinessService businessService)
        {
            _businessService = businessService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Business>>> GetAllBusiness()
        {
            var businesses = await _businessService.GetAllBusinessAsync();
            return Ok(businesses);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Business>> GetBusinessById(int id)
        {
            var business = await _businessService.GetBusinessByIdAsync(id);
            if (business == null)
            {
                return NotFound();
            }
            return Ok(business);
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddBusiness(Business business)
        {
            var newbusiness = await _businessService.AddBusinessAsync(business);

            // Retornar el ID del nuevo negocio con un código 200
            return Ok(newbusiness);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBusiness(int id, Business business)
        {
            // Verificar si el id en la ruta coincide con el BusinessID en el objeto
            if (id != business.BusinessID)
            {
                return BadRequest("El ID del negocio no coincide.");
            }

            // Llamada al servicio para actualizar el negocio
            var updatedBusiness = await _businessService.UpdateBusinessAsync(business);

            // Si no se encuentra el negocio o no se pudo actualizar
            if (updatedBusiness == null)
            {
                return NotFound("Negocio no encontrado.");
            }

            // Si la actualización fue exitosa, retornamos el negocio actualizado
            return Ok(updatedBusiness);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBusiness(int id)
        {
            var deleted = await _businessService.DeleteBusinessAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
