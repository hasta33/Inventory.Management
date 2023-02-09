using InventoryManagement.Core.DTOs;
using InventoryManagement.Core.DTOs.CategorySub;
using InventoryManagement.Core.Models;

namespace InventoryManagement.Core.Services
{
    public interface ICategorySubServiceWithDto : IServiceWithDto<CategorySub, CategorySubDto>
    {
        Task<CustomResponseDto<NoContent>> UpdateAsync(CategorySubUpdateDto dto);
        Task<CustomResponseDto<CategorySubDto>> AddAsync(CategorySubCreateDto dto);
    }
}
