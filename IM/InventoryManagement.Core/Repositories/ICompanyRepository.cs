using InventoryManagement.Core.Models;

namespace InventoryManagement.Core.Repositories
{
    public interface ICompanyRepository : IGenericRepository<Company>
    {
        Task<List<Company>> GetCompanyList(int page, int pageSize);
        Task<List<Company>> GetCompanyWithCategoryListAsync(int businessCode);
        Task<List<Company>> GetCompanyOnlyNameAndBusinessCode();
    }
}
