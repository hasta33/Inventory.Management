using InventoryManagement.Core.DTOs;
using InventoryManagement.Core.DTOs.Category;
using InventoryManagement.Core.DTOs.Company;
using InventoryManagement.Core.Models;

namespace InventoryManagement.Core.Services
{
    /* CustomResponseDto şeklinde yapmak için */
    public interface ICategoryServiceWithDto : IServiceWithDto<Category, CategoryDto>
    {
        Task<CustomResponseDto<List<CategoryDto>>> GetCategories();
        Task<CustomResponseDto<List<CategoryDto>>> GetCategoriesById(int id);
        Task<CustomResponseDto<List<CategoryDto>>> GetCategoriesPageList(int page, int pageSize);


        Task<CustomResponseDto<NoContent>> UpdateAsync(CategoryUpdateDto dto);
        Task<CustomResponseDto<CategoryDto>> AddAsync(CategoryCreateDto dto);
    }
}
