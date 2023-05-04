using InventoryManagement.Core.Models;
using InventoryManagement.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Repository.Repositories
{
    public class InventoryMovementRepository : GenericRepository<InventoryMovement>, IInventoryMovementRepository
    {
        public InventoryMovementRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<InventoryMovement>> GetInventoryMovements(int inventoryId)
        {
            return await _context.InventoryMovements
            //.Where(b => b.BusinessCode == businessCode)
               .Where(c => c.InventoryId == inventoryId)
              //.Include(x => x.CategorySubs.Where(x => x.CategoryId == companyId))  //kategoriye ait alt kategorileri listeleme
              .OrderByDescending(x => x.CreatedDate)
              .ToListAsync();
        }
    }
}
