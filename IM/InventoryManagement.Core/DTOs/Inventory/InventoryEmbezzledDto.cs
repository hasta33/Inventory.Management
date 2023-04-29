namespace InventoryManagement.Core.DTOs.Inventory
{
    public class InventoryEmbezzledDto
    {
        public int Id { get; set; }
        public int Barcode { get; set; }
        public string SerialNumber { get; set; } = string.Empty;
        public int Imei { get; set; }
        public string Mac { get; set; } = string.Empty;


        public string Responsible { get; set; } = string.Empty;
    }
}
