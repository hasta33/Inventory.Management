namespace InventoryManagement.Core.DTOs.Company
{
    public class CompanyAllDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int BusinessCode { get; set; }

        public int TotalCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
