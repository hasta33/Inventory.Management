using InventoryManagement.Core.Models;

namespace InventoryManagement.Core.Repositories
{
    public interface IBrandRepository : IGenericRepository<Brand>
    {
        Task<List<Brand>> GetBrandList(int companyId, int page, int pageSize);
        Task<List<Brand>> GetBrandList(int companyId);
        Task<List<Brand>> GetBrandAllList();
    }
}
