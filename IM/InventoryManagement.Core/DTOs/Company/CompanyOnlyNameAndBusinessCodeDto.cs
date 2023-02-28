namespace InventoryManagement.Core.DTOs.Company
{
    public class CompanyOnlyNameAndBusinessCodeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string BusinessCode { get; set; } = string.Empty;
    }
}
