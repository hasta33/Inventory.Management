namespace InventoryManagement.Core.DTOs
{
    public class FilteringParameters
    {
        public int? CompanyId { get; set; }
        public int? CategoryId { get; set; }
        public int? CategorySubId { get; set; }
        public int? BrandId { get; set; }
        public int? ModelId { get; set; }
        public string? Name { get; set; }

        public int? Barcode { get; set; }
        public string? SerialNumber { get; set; }
        public string? Mac { get; set; }

        public int? Imei { get; set; }

        public string? Embezzled { get; set; }
        public string? Status { get; set; }
    }
}
