using InventoryManagement.Core.DTOs;
using InventoryManagement.Core.DTOs.Company;
using InventoryManagement.Core.Models;

namespace InventoryManagement.Core.Services
{
    public interface ICompanyServiceWithDto : IServiceWithDto<Company, CompanyDto>
    {
        Task<CustomResponseDto<CompanyDto>> AddAsync(CompanyCreateDto dto);
        Task<CustomResponseDto<NoContent>> UpdateAsync(CompanyUpdateDto dto);



        Task<CustomResponseDto<List<CompanyAllDto>>> GetCompanyAllList(int page, int pageSize);
        Task<CustomResponseDto<List<CompanyDto>>> GetCompanyListWithSubTables(int companyId, int page, int pageSize);


        //Task<CustomResponseDto<List<CompanyDto>>> GetCompanyWithCategoryListAsync(int businessCode);
    }
}
