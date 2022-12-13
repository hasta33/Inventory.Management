using InventoryManagement.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Repository
{
    public class DataContext : DbContext
    {

        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }


        public DbSet<Company> Companies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BrandModel> Brands { get; set; }





    }
}
