using InventoryManagement.Core.DTOs.CategorySub;

namespace InventoryManagement.Core.DTOs.Category
{
    public class CategoryWithCategorySubDto : CategoryDto
    {
        public List<CategorySubDto> CategorySubs { get; set; }
    }
}
