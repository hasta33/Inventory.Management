using InventoryManagement.Core.Models;
using InventoryManagement.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Repository.Repositories
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(DataContext context) : base(context)
        {

        }

        public async Task<List<Company>> GetCompanyListWithSubTables(int companyId, int page, int pageSize)
        {
            IQueryable<Company> query;

            query = _context.Companies
                .Where(x => x.Id == companyId)
                .Include(x => x.Categories.Where(x => x.CompanyId == companyId))
                .Include(x => x.Categories.Where(x => x.CompanyId == companyId))
                .ThenInclude(x => x.CategorySubs)

                .Include(x => x.Brands.Where(x => x.CompanyId == companyId))
                .ThenInclude(x => x.Models)

                .Include(x => x.Inventories.Where(x => x.CompanyId == companyId)) //sadece envanter kayıtlarını getir

                .OrderByDescending(x => x.CreatedDate);
            int totalCount = query.Count();

            var response = await query.Skip(pageSize * (page - 1))
                .Take(pageSize)
                .Select(x => new Company()
                {
                    BusinessCode = x.BusinessCode,
                    CreatedDate = Convert.ToDateTime(x.CreatedDate),
                    Description = x.Description,
                    Id = x.Id,
                    Name = x.Name,
                    UpdatedDate = Convert.ToDateTime(x.UpdatedDate),
                    Categories = x.Categories,
                    TotalCount = totalCount,
                    Brands = x.Brands,
                    Inventories = x.Inventories,
                }).ToListAsync();

            return response;
        }

        public async Task<List<Company>> GetCompanyAllList(int page, int pageSize)
        {
            IQueryable<Company> query;

            query = _context.Companies
                .OrderByDescending(x => x.CreatedDate);

            int totalCount = query.Count();

            var response = await query.Select(x => new Company()
            {
                Id = x.Id,
                BusinessCode = x.BusinessCode,
                Description = x.Description,
                Name = x.Name,
                TotalCount = totalCount,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Categories = x.Categories,
                Brands = x.Brands
            }).ToListAsync();

            return response;
        }

        //public async Task<List<Company>> GetCompanyWithCategoryListAsync(int businessCode)
        //{
        //    return await _context.Companies
        //        .Where(b => b.BusinessCode == businessCode)
        //        .Include(c => c.Categories.Where(x => x.BusinessCode == businessCode)).ThenInclude(x => x.CategorySubs)

        //        .OrderByDescending(x => x.CreatedDate)
        //        .ToListAsync();
        //}
    }
}
