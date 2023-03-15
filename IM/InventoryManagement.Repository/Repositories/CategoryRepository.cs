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

        public async Task<List<Category>> GetCategoryList(int companyId)
        {
            return await _context.Categories
                 //.Where(b => b.BusinessCode == businessCode)
                 .Where(c => c.CompanyId == companyId)
                .Include(x => x.CategorySubs.Where(x => x.CategoryId == companyId))  //kategoriye ait alt kategorileri listeleme
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }

        public async Task<List<Category>> GetCategoryList(int companyId, int page, int pageSize)
        {
            IQueryable<Category> query;
            query = _context.Categories
                .Where(x => x.CompanyId == companyId)
                .Include(x => x.CategorySubs.Where(x => x.CategoryId == companyId))
                .OrderByDescending(x => x.CreatedDate);

            int totalCount = query.Count();

            var response = await query.Skip((pageSize * (page - 1)))
                .Take(pageSize)
                .Select(x => new Category()
                {
                    Id = x.Id,
                    Name = x.Name,
                    CompanyId = x.CompanyId,
                    UpdatedDate = x.UpdatedDate,
                    CreatedDate = x.CreatedDate,
                    CategorySubs = x.CategorySubs,
                    TotalCount = totalCount
                }).ToListAsync();

            return response;
        }
    }
}
