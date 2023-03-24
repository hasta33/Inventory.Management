namespace InventoryManagement.Core.Models
{
    public class InventoryMovement : BaseEntity
    {
        public int InventoryId { get; set; }
        public Inventory Inventory { get; set; } = default!;


        public string Process { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string ResponsibleUser { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
