using InventoryManagement.Core.Models;
using InventoryManagement.Core.Repositories;

namespace InventoryManagement.Repository.Repositories
{
    public class CategorySubRepository : GenericRepository<CategorySub>, ICategorySubRepository
    {
        public CategorySubRepository(DataContext context): base(context)
        {

        }
    }
}
