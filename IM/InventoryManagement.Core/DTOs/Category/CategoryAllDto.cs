namespace InventoryManagement.Core.DTOs.Category
{
    public class CategoryAllDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int BusinessCode { get; set; }
        public int TotalCount { get; set; }
        public int CompanyId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
