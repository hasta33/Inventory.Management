using InventoryManagement.Core.DTOs;
using InventoryManagement.Core.DTOs.Category;
using InventoryManagement.Core.Models;

namespace InventoryManagement.Core.Services
{
    public interface ICategoryServiceWithDto : IServiceWithDto<Category, CategoryDto>
    {
        Task<CustomResponseDto<List<CategoryWithCategorySubDto>>> GetCategoryByIdWithSubCategory(int id);

        
        //Custom ekleme
        Task<CustomResponseDto<NoContent>> UpdateAsync(CategoryUpdateDto dto);
        Task<CustomResponseDto<CategoryDto>> AddAsync(CategoryCreateDto dto);
    }
}
