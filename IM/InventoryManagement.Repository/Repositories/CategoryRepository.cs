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

        public async Task<List<Category>> GetCategoryList(int page, int pageSize)
        {
            IQueryable<Category> query;
            query = _context.Categories
                .OrderByDescending(x => x.CreatedDate);

            int totalCount = query.Count();

            var response = await query.Skip((pageSize * (page - 1)))
                .Take(pageSize)
                .Select(x => new Category()
                {
                    Id = x.Id,
                    Name = x.Name,
                    BusinessCode = x.BusinessCode,
                    CompanyId = x.CompanyId,
                    UpdatedDate = x.UpdatedDate,
                    CreatedDate = x.CreatedDate,
                    TotalCount = totalCount

                }).ToListAsync();

            return response;
        }
    }
}
