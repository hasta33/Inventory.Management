namespace InventoryManagement.Core.DTOs.BrandModel
{
    public class BrandModelCreateDto
    {
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}