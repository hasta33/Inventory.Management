using InventoryManagement.Core.UnitOfWork;

namespace InventoryManagement.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly DataContext _context;
        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
