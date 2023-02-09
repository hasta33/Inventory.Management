namespace InventoryManagement.Core.DTOs.CategorySub
{
    public class CategorySubCreateDto
    {
        public string Name { get; set; } = string.Empty; 
        public int BusinessCode { get; set; }




        #region Relationship - Affiliated with the upper class
        public int CategoryId { get; set; }
        #endregion
    }
}
