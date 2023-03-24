using InventoryManagement.Core.Models;

namespace InventoryManagement.Core.Repositories
{
    public interface IInventoryMovementRepository : IGenericRepository<InventoryMovement>
    {
        Task<List<InventoryMovement>> GetInventoryMovements(int inventoryId);
    }
}
