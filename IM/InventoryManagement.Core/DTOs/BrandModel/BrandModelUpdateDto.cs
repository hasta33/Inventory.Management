namespace InventoryManagement.Core.DTOs.BrandModel
{
    public class BrandModelUpdateDto
    {
        public int Id { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}