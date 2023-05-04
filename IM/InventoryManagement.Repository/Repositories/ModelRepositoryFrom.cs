using InventoryManagement.Core.Models;
using InventoryManagement.Core.Repositories;

namespace InventoryManagement.Repository.Repositories
{
    public class ModelRepositoryFrom : GenericRepository<Model>, IModelRepository
    {
        public ModelRepositoryFrom(DataContext context) : base(context)
        {
        }
    }
}
