using InventoryManagement.Core.Models;

namespace InventoryManagement.Core.Repositories
{
    public interface ICompanyRepository : IGenericRepository<Company>
    {
        Task<List<Company>> GetCompanyListWithSubTables(int companyId, int page, int pageSize);
        //Task<List<Company>> GetCompanyWithCategoryListAsync(int businessCode);
        Task<List<Company>> GetCompanyAllList(int page, int pageSize);
    }
}
