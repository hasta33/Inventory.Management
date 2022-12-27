using InventoryManagement.Core.DTOs.Category;
using InventoryManagement.Core.Models;

namespace InventoryManagement.Core.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<List<Category>> GetCategories();
        Task<List<Category>> GetCategoriesById(int id);
        Task<List<CategoryDto>> GetCategoriesList(int page, int pageSize);
    }
}
