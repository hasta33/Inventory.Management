using InventoryManagement.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Repository.Seeds
{
    public class CompanySeed : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasData(
                 new Company
                 {
                     Id = 1,
                     Name = "Enerya İstanbul Gaz Dağıtım A.Ş.",
                     Description = "Enerya istanbul",
                     BusinessCode = 3000
                 },
                new Company
                {
                    Id = 2,
                    Name = "Enerya Ereğli Gaz Dağıtım A.Ş.",
                    Description = "Enerya ereğli",
                    BusinessCode = 3100
                },
                 new Company
                 {
                     Id = 3,
                     Name = "Enerya Konya Gaz Dağıtım A.Ş.",
                     Description = "Enerya konya",
                     BusinessCode = 3200
                 }
            );
        }
    }
}
