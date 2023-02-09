using InventoryManagement.Core.DTOs;
using InventoryManagement.Core.DTOs.Company;
using InventoryManagement.Core.Models;

namespace InventoryManagement.Core.Services
{
    public interface ICompanyServiceWithDto : IServiceWithDto<Company, CompanyDto>
    {
        Task<CustomResponseDto<List<CompanyDto>>> GetCompanyList(int page, int pageSize, int businessCode);
        Task<CustomResponseDto<List<CompanyDto>>> GetCompanyWithCategoryListAsync(int businessCode);
        

        Task<CustomResponseDto<NoContent>> UpdateAsync(CompanyUpdateDto dto);
        Task<CustomResponseDto<CompanyDto>> AddAsync(CompanyCreateDto dto);
    }
}
