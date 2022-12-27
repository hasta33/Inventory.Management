using InventoryManagement.Core.DTOs.Category;
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

        public async Task<List<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<List<Category>> GetCategoriesById(int id)
        {
            return await _context.Categories.Where(x => x.Id == id).OrderByDescending(x => x.CreatedDate).ToListAsync();
        }

        public async Task<List<CategoryDto>> GetCategoriesList(int page, int pageSize)
        {
            IQueryable<Category> query;
            query = _context.Categories
                .OrderByDescending(x => x.CreatedDate);

            int totalCount = query.Count();
            var response = await query.Skip((pageSize * (page - 1)))
                .Take(pageSize)
                .Select(x => new CategoryDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreatedDate = x.CreatedDate,
                    UpdatedDate = x.UpdatedDate,
                }).ToListAsync();
            return response;
        }
    }
}
