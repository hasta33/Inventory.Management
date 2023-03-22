using InventoryManagement.Core.DTOs;
using InventoryManagement.Core.DTOs.Brand;
using InventoryManagement.Core.Models;

namespace InventoryManagement.Core.Services
{
    public interface IBrandServiceWithDto : IServiceWithDto<Brand, BrandDto>
    {
        Task<CustomResponseDto<List<BrandDto>>> GetBrandList(int companyId, int page, int pageSize);
        Task<CustomResponseDto<List<BrandDto>>> GetBrandList(int companyId);
        Task<CustomResponseDto<List<BrandDto>>> GetBrandAllList();

        Task<CustomResponseDto<NoContent>> UpdateAsync(BrandUpdateDto dto);
        Task<CustomResponseDto<BrandDto>> AddAsync(BrandCreateDto dto);
    }
}
