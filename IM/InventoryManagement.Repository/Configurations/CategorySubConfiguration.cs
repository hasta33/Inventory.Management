using InventoryManagement.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Repository.Configurations
{
    public class CategorySubConfiguration : IEntityTypeConfiguration<CategorySub>
    {
        public void Configure(EntityTypeBuilder<CategorySub> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(250);
            builder.Property(x => x.CategoryId).IsRequired();
            
            builder.ToTable(nameof(CategorySub));

            //Relationship
            builder.HasOne(x => x.Category).WithMany(x => x.CategorySubs).HasForeignKey(x => x.CategoryId);
        }
    }
}
