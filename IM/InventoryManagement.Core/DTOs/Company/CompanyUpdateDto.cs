namespace InventoryManagement.Core.DTOs.Company
{
    public class CompanyUpdateDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int BusinessCode { get; set; }
    }
}
