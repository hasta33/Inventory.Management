namespace InventoryManagement.Core.DTOs.Brand
{
    public class BrandUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CompanyId { get; set; }
    }
}
