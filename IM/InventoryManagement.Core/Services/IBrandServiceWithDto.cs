using InventoryManagement.Core.DTOs;
using InventoryManagement.Core.DTOs.BrandModel.Brand;
using InventoryManagement.Core.Models;

namespace InventoryManagement.Core.Services
{
    public interface IBrandServiceWithDto : IServiceWithDto<Brand, BrandDto>
    {
        Task<CustomResponseDto<List<BrandWithModelDto>>> GetBrandByIdWithModel(int id);



        //Custom
        Task<CustomResponseDto<NoContent>> UpdateAsync(BrandUpdateDto dto);
        Task<CustomResponseDto<BrandDto>> AddAsync(BrandCreateDto dto);
    }
}
