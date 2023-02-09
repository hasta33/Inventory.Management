using InventoryManagement.Core.DTOs.CategorySub;

namespace InventoryManagement.Core.DTOs.Category
{
    public class CategoryDto : BaseDto
    {
        public string Name { get; set; } = string.Empty;



        #region Relationship - Affiliated with the upper class
        public int CompanyId { get; set; }
        //public int CategoryId { get; set; }
        #endregion




        #region Relationship - SubClass
        public List<CategorySubDto>? CategorySubs { get; set; }
        #endregion
    }
}
