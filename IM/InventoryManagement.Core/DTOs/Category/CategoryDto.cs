using InventoryManagement.Core.DTOs.CategorySub;

namespace InventoryManagement.Core.DTOs.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int BusinessCode { get; set; }
        public int TotalCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }





        #region Relationship - Affiliated with the upper class
        public int CompanyId { get; set; }
        //public int CategoryId { get; set; }
        #endregion






        #region Relationship - SubClass
        public List<CategorySubDto>? CategorySubs { get; set; }
        #endregion
    }
}
