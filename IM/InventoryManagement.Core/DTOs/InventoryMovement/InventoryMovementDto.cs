namespace InventoryManagement.Core.DTOs.InventoryMovement
{
    public class InventoryMovementDto
    {
        #region Relationship - Affiliated with the upper class
        public int InventoryId { get; set; }
        #endregion


        public int Id { get; set; }
        public string Process { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string ResponsibleUser { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;



        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
