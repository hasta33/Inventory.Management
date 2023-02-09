﻿using InventoryManagement.Core.Models;
using InventoryManagement.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Repository.Repositories
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<Company>> GetCompanyList(int page, int pageSize, int businessCode)
        {
            IQueryable<Company> query;
            query = _context.Companies
                .Where(x => x.BusinessCode == businessCode)
                .OrderByDescending(x => x.CreatedDate);
            int totalCount = query.Count();
            var response = await query.Skip((pageSize * (page - 1)))
                .Take(pageSize)
                .Select(x => new Company()
                {
                    BusinessCode = x.BusinessCode,
                    CreatedDate = Convert.ToDateTime(x.CreatedDate),
                    Description = x.Description,
                    Id = x.Id,
                    Name = x.Name,
                    UpdatedDate = Convert.ToDateTime(x.UpdatedDate),
                    TotalCount = totalCount,
                }).ToListAsync();

            return response;
        }

        public async Task<List<Company>> GetCompanyWithCategoryListAsync(int businessCode)
        {
            return await _context.Companies
                .Where(b => b.BusinessCode == businessCode)
                .Include(c => c.Categories.Where(x => x.BusinessCode == businessCode)).ThenInclude(x => x.CategorySubs)

                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }
    }
}
