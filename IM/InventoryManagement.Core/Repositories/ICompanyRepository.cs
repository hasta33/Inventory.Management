using InventoryManagement.Core.DTOs.Company;
using InventoryManagement.Core.Models;

namespace InventoryManagement.Core.Repositories
{
    public interface ICompanyRepository : IGenericRepository<Company>
    {
        Task<List<Company>> GetCompanies();
        Task<List<Company>> GetCompaniesById(int id);
        Task<List<CompanyDto>> GetCompaniesList(int page, int pageSize);
    }
}
