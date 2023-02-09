using InventoryManagement.Core.Models;
using InventoryManagement.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Repository.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DataContext context) : base(context)
        {

        }

        public async Task<List<Category>> GetCategoryList(int businessCode)
        {
            return await _context.Categories
                .Where(b => b.BusinessCode == businessCode)
                .Include(x => x.CategorySubs.Where(x => x.BusinessCode == businessCode))
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }
    }
}
