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

        //public async Task<List<Category>> GetCategoryByIdWithSubCategory(int id)
        //{
        //    //return await _context.CategoriesSub.Include(x => x.Category).Where(x => x.Id == id).SingleOrDefaultAsync();
        //    return await _context.CategoriesSub.Include(c => c.CategoryId).ToListAsync();
        //}

        public async Task<List<Category>> GetCategoryByIdWithSubCategory(int id)
        {
            //return await _context.CategoriesSub.Include(x => x.Category).Where(x => x.Id == id).SingleOrDefaultAsync();
            //return await _context.Categories.Where(x => x.Id == id).ToListAsync();

            return await _context.Categories.Where(x => x.Id == id).ToListAsync();
            //return await _context.Categories.Where(x => x.Id == id).Include(x => x.CategorySubs).ToListAsync();
        }
    }
}
