using KeyVaultApi.Application.DTOs.Response;
using KeyVaultApi.Application.Interfaces;
using KeyVaultApi.Application.Interfaces.Services;
using KeyVaultApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyVaultApi.Application.Services
{
    public class BusinessService : IBusinessService
    {

        private readonly IBusinessRepository _businessRespository;

        public BusinessService(IBusinessRepository businessRepository)
        {
            _businessRespository = businessRepository;
        }

        public async Task<Business> AddBusinessAsync(Business business)
        {
            return await _businessRespository.AddBusinessAsync(business);
        }

        public async Task<bool> DeleteBusinessAsync(int id)
        {
            return await _businessRespository.DeleteBusinessAsync(id);
        }

        public async Task<IEnumerable<Business>> GetAllBusinessAsync()
        {
            return await _businessRespository.GetAllBusinessAsync();
        }

        public async Task<Business> GetBusinessByIdAsync(int id)
        {
            return await _businessRespository.GetBusinessByIdAsync(id);
        }

        public async Task<UpdateBussinessResponse> UpdateBusinessAsync(Business business)
        {
            var updatedBussiness = await _businessRespository.UpdateBusinessAsync(business);

            var responsBussiness = new UpdateBussinessResponse();

            responsBussiness.UserName = business.UserName;
            responsBussiness.Address = business.Address;
            responsBussiness.Phone = business.Phone;
            responsBussiness.Email = business.Email;
            responsBussiness.Active = business.Active;

            return responsBussiness;
        }
    }
}
