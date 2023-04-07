namespace InventoryManagement.Core.DTOs.InventoryMovement
{
    public class InventoryMovementCreateDto
    {
        public int InventoryId { get; set; }


        //public string Process { get; set; } = string.Empty;
        //public string Company { get; set; } = string.Empty;
        //public string ResponsibleUser { get; set; } = string.Empty;
        //public string Description { get; set; } = string.Empty;

        public string Perpetrator { get; set; } = string.Empty;
        public string Process { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string EmbezzledUser { get; set; } = string.Empty;
    }
}
