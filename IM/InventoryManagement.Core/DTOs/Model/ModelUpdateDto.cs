namespace InventoryManagement.Core.DTOs.Model
{
    public class ModelUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int BrandId { get; set; }
    }
}