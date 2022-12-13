namespace InventoryManagement.Core.DTOs.Company
{
    public class CompanyCreateDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
