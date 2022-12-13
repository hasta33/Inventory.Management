using InventoryManagement.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Repository.Seeds
{
    public class CompanySeed : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasData(new Company
            {
                Id= 1,
                Name = "Enerya Enerji A.Ş.",
                Description = "Enerya genel merkez",
                CreatedDate = DateTime.Now,
                UpdatedDate = null
            });
        }
    }
}
