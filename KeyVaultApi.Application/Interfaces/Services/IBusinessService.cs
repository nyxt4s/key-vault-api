using KeyVaultApi.Application.DTOs.Response;
using KeyVaultApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyVaultApi.Application.Interfaces.Services
{
    public interface IBusinessService
    {
        Task<IEnumerable<Business>> GetAllBusinessAsync();
        Task<Business> GetBusinessByIdAsync(int id);
        Task<Business> AddBusinessAsync(Business business);
        Task<UpdateBussinessResponse> UpdateBusinessAsync(Business business);
        Task<bool> DeleteBusinessAsync(int id);
    }
}
 