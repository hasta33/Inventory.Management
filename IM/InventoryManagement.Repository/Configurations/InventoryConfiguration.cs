using InventoryManagement.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Repository.Configurations
{
    public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.CompanyId).IsRequired();
            builder.Property(x => x.CategoryId).IsRequired();
            builder.Property(x => x.CategorySubId).IsRequired();
            builder.Property(x => x.BrandId).IsRequired();
            builder.Property(x => x.ModelId).IsRequired();
            
            builder.Property(x => x.Name).HasMaxLength(250);
            builder.Property(x => x.Barcode).HasMaxLength(64);
            builder.Property(x => x.SerialNumber).HasMaxLength(250);
            builder.Property(x => x.Imei).HasMaxLength(250);
            builder.Property(x => x.Mac).HasMaxLength(250);
            builder.Property(x => x.Status).HasMaxLength(250);
            builder.Property(x => x.Embezzled).HasMaxLength(250);


            builder.ToTable(nameof(Inventory));

            //Relationship
            builder.HasOne(x => x.Company).WithMany(x => x.Inventories).HasForeignKey(x => x.CompanyId);
        }
    }
}
