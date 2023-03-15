using InventoryManagement.Core.Models;
using InventoryManagement.Core.Repositories;

namespace InventoryManagement.Repository.Repositories
{
    public class ModelRepository : GenericRepository<Model>, IModelRepository
    {
        public ModelRepository(DataContext context) : base(context)
        {
        }
    }
}
