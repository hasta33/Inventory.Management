using InventoryManagement.Core.Models;

namespace InventoryManagement.Core.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<List<Category>> GetCategoryList(int companyId, int page, int pageSize);
        Task<List<Category>> GetCategoryList(int companyId);
    }
}
