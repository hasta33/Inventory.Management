using InventoryManagement.Core.DTOs;
using InventoryManagement.Core.DTOs.Company;
using InventoryManagement.Core.Models;

namespace InventoryManagement.Core.Services.Entity
{
    /* Entity şeklinde yapmak için */
    public interface ICompanyService : IService<Company>
    {
        Task<CustomResponseDto<List<CompanyDto>>> GetCompanies();
        Task<CustomResponseDto<List<CompanyDto>>> GetCompaniesById(int id);
        Task<CustomResponseDto<List<CompanyDto>>> GetCompaniesPageList(int page, int pageSize);
    }
}
