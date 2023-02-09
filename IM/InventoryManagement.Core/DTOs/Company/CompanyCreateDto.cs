namespace InventoryManagement.Core.DTOs.Company
{
    public class CompanyCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int BusinessCode { get; set; }
    }
}
