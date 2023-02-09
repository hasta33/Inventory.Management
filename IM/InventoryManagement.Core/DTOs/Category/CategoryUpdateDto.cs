namespace InventoryManagement.Core.DTOs.Category
{
    public class CategoryUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int BusinessCode { get; set; }
        public int CompanyId { get; set; }
    }
}
