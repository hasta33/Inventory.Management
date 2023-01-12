using InventoryManagement.Core.Models;

namespace InventoryManagement.Core.Repositories
{
    public interface IBrandRepository : IGenericRepository<Brand>
    {
        Task<List<Brand>> GetBrandByIdWithModel(int id);
    }
}
