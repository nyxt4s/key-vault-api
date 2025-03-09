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
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<IEnumerable<Brand>> GetAllBrandsAsync()
        {
            return await _brandRepository.GetAllBrandsAsync();
        }

        public async Task<Brand> GetBrandByIdAsync(int id)
        {
            return await _brandRepository.GetBrandByIdAsync(id);
        }

        public async Task<int> AddBrandAsync(Brand brand)
        {
            return await _brandRepository.AddBrandAsync(brand);
        }

        public async Task<bool> UpdateBrandAsync(Brand brand)
        {
            return await _brandRepository.UpdateBrandAsync(brand);
        }

        public async Task<bool> DeleteBrandAsync(int id)
        {
            return await _brandRepository.DeleteBrandAsync(id);
        }
    }
}

