using InventoryManagement.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Repository.Configurations
{
    public class CategorySubConfiguration : IEntityTypeConfiguration<CategorySub>
    {
        public void Configure(EntityTypeBuilder<CategorySub> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).UseIdentityColumn();
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.ToTable("CategorySub");
        }
    }
}
