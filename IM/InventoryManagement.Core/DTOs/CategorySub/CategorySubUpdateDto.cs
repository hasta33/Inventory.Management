namespace InventoryManagement.Core.DTOs.CategorySub
{
    public class CategorySubUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int BusinessCode { get; set; }
        public int CategoryId { get; set; }
    }
}
