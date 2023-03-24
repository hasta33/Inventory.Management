using InventoryManagement.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Repository.Configurations
{
    public class InventoryMovementConfiguration : IEntityTypeConfiguration<InventoryMovement>
    {
        public void Configure(EntityTypeBuilder<InventoryMovement> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.InventoryId).IsRequired();
            builder.Property(x => x.Process).HasMaxLength(250);
            builder.Property(x => x.Company).HasMaxLength(250);
            builder.Property(x => x.ResponsibleUser).HasMaxLength(250);
            builder.Property(x => x.Description).HasMaxLength(250);

            builder.ToTable(nameof(InventoryMovement));

            //relationship
            builder.HasOne(x => x.Inventory).WithMany(x => x.InventoryMovements).HasForeignKey(x => x.InventoryId);
        }
    }
}
