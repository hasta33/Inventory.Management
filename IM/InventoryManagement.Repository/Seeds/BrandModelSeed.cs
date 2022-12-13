using InventoryManagement.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Repository.Seeds
{
    public class BrandModelSeed : IEntityTypeConfiguration<BrandModel>
    {
        public void Configure(EntityTypeBuilder<BrandModel> builder)
        {
            builder.HasData(new BrandModel
            {
                Id= 1,
                Brand = "Samsung",
                Model = "A3",
                CreatedDate= DateTime.Now,
                UpdatedDate = null
            });
        }
    }
}
