using InventoryManagement.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;

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
        public DbSet<CategorySub> CategoriesSub { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

            ////Many-to-many relationship cascade properties
            //foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            //{
            //    relationship.DeleteBehavior = DeleteBehavior.NoAction;
            //}
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=IMDB;User=sa;Password=Aa123456789*-+;TrustServerCertificate=True");
            base.OnConfiguring(optionsBuilder);
        }


        public override int SaveChanges()
        {
            //foreach (var item in ChangeTracker.Entries())
            //{
            //    if (item.Entity is BaseEntity entityReference)
            //    {
            //        switch (item.Entity)
            //        {
            //            case EntityState.Added:
            //                {
            //                    Entry(entityReference).Property(x => x.UpdatedDate).IsModified = false;
            //                    entityReference.CreatedDate = DateTime.UtcNow;
            //                    break;
            //                }
            //            case EntityState.Modified:
            //                {
            //                    entityReference.UpdatedDate = DateTime.UtcNow;
            //                    break;
            //                }
            //        }
            //    }
            //}
            UpdateChangeTracker();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //Console.WriteLine(ChangeTracker.Entries());

            UpdateChangeTracker();
          
            return base.SaveChangesAsync(cancellationToken);
        }

        public void UpdateChangeTracker()
        {
            foreach (var item in ChangeTracker.Entries())
            {

                if (item.Entity is BaseEntity entityReference)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {
                                Entry(entityReference).Property(x => x.UpdatedDate).IsModified = false;
                                entityReference.CreatedDate = DateTime.UtcNow;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                Entry(entityReference).Property(x => x.CreatedDate).IsModified = false;
                                entityReference.UpdatedDate = DateTime.Now;
                                break;
                            }
                    }
                }
            }
        }




    }
}
