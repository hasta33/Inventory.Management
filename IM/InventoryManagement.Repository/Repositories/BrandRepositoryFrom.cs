using InventoryManagement.Core.Models;
using InventoryManagement.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Repository.Repositories
{
    public class BrandRepositorySqlServer : GenericRepository<Brand>, IBrandRepository
    {
        public BrandRepositorySqlServer(DataContext context) : base(context)
        {

        }

        public async Task<List<Brand>> GetBrandAllList()
        {
            return await _context.Brands.Include(x => x.Models)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }

        public async Task<List<Brand>> GetBrandList(int companyId, int page, int pageSize)
        {
            IQueryable<Brand> query;
            query = _context.Brands
                .Where(x => x.CompanyId == companyId)
                //.Include(x => x.Models.Where(x => x.BrandId == companyId))
                .OrderByDescending(x => x.CreatedDate);

            int totalCount = query.Count();

            var response = await query.Skip(pageSize * (page - 1))
                .Take(pageSize)
                .Select(x => new Brand()
                {
                    CompanyId = companyId,
                    CreatedDate = x.CreatedDate,
                    BusinessCode = x.BusinessCode,
                    Id = x.Id,
                    Name = x.Name,
                    TotalCount = totalCount,
                    UpdatedDate = x.UpdatedDate,
                    Models = x.Models
                }).ToListAsync();

            return response;
        }

        public async Task<List<Brand>> GetBrandList(int companyId)
        {
            //return await _context.Brands
            //    .Where(x => x.CompanyId == companyId)
            //    .Include(x => x.Models.Where(x => x. BrandId == companyId))
            //    .OrderByDescending(x => x.CreatedDate)
            //    .ToListAsync();

            IQueryable<Brand> query;
            query = _context.Brands
                .Where(x => x.CompanyId == companyId)
                .Include(x => x.Models.Where(x => x.BrandId == companyId)
                .OrderByDescending(x => x.CreatedDate));

            int totalCount = query.Count();

            var response = await query
                .Select(x => new Brand()
                {
                    Id = x.Id,
                    CompanyId = companyId,
                    CreatedDate = x.CreatedDate,
                    BusinessCode = x.BusinessCode,
                    Name = x.Name,
                    TotalCount = totalCount,
                    UpdatedDate = x.UpdatedDate,
                    Models = x.Models
                }).ToListAsync();
            return response;
        }
    }
}
