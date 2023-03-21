namespace InventoryManagement.Core.DTOs.Inventory
{
    public class InventoryAllDto
    {
        public int Id { get; set; }
        #region Relationship - Affiliated with the upper class
        public int CompanyId { get; set; }
        public int CategoryId { get; set; }
        public int CategorySubId { get; set; }
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        #endregion


        public string Name { get; set; } = string.Empty;
        public DateTime InventoryDate { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int Barcode { get; set; }
        public string SerialNumber { get; set; } = string.Empty;
        public int Imei { get; set; }
        public string Mac { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Responsible { get; set; } = string.Empty;


        public int TotalCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
