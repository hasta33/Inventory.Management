using InventoryManagement.Core.DTOs.Company;
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

        public async Task<List<Company>> GetCompanies()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<List<Company>> GetCompaniesById(int id)
        {
            return await _context.Companies.Where(x => x.Id == id).OrderByDescending(x => x.CreatedDate).ToListAsync();
        }

        public async Task<List<CompanyDto>> GetCompaniesList(int page, int pageSize)
        {
            IQueryable<Company> query;
            query = _context.Companies
                .OrderByDescending(x => x.CreatedDate);

            int totalCount = query.Count();
            var response = await query.Skip((pageSize * (page - 1)))
                .Take(pageSize)
                .Select(x => new CompanyDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreatedDate = x.CreatedDate,
                    Description = x.Description,
                    UpdatedDate = x.UpdatedDate,
                }).ToListAsync();
            return response;
        }
    }
}
