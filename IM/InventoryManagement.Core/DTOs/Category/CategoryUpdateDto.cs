namespace InventoryManagement.Core.DTOs.Category
{
    public class CategoryUpdateDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
