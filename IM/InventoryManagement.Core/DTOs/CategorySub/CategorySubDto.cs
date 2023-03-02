namespace InventoryManagement.Core.DTOs.CategorySub
{
    public class CategorySubDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }




        #region Relationship - Affiliated with the upper class
        public int CategoryId { get; set; }
        #endregion
    }
}
