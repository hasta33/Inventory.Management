using InventoryManagement.Core.DTOs.Brand;
using InventoryManagement.Core.DTOs.Category;

namespace InventoryManagement.Core.DTOs.Company
{
    public class CompanyDto : BaseDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;


        public List<CategoryDto>? Categories { get; set; }
        public List<BrandDto>? Brands { get; set; }
    }
}
