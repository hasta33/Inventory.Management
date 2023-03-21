using InventoryManagement.Core.DTOs;
using InventoryManagement.Core.Models;

namespace InventoryManagement.Core.Repositories
{
    public interface IInventoryRepository : IGenericRepository<Inventory>
    {
        //Task<List<Inventory>> GetInventoryList(int companyId, int page, int pageSize);
        Task<List<Inventory>> GetInventoryList(FilteringParameters parameters, int page, int pageSize);
    }

}
