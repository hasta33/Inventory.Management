using InventoryManagement.Core.Models;
using InventoryManagement.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Repository.Repositories
{
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        public BrandRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<Brand>> GetBrandByIdWithModel(int id)
        {
            return await _context.Brands.Include(x => x.Models).Where(x => x.Id == id).ToListAsync();
        }
    }
}
