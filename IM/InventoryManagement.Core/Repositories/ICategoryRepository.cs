using InventoryManagement.Core.Models;

namespace InventoryManagement.Core.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<List<Category>> GetCategoryByIdWithSubCategory(int id);
    }
}
