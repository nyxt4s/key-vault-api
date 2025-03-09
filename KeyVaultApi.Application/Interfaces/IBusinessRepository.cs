using KeyVaultApi.Application.DTOs.Response;
using KeyVaultApi.Domain.Entities;


namespace KeyVaultApi.Application.Interfaces
{
    public interface IBusinessRepository
    {
        Task<List<Business>> GetAllBusinessAsync();
        Task<Business> GetBusinessByIdAsync(int id);
        Task<Business> AddBusinessAsync(Business business);
        Task<Business> UpdateBusinessAsync(Business business);
        Task<bool> DeleteBusinessAsync(int id);
    }
}
