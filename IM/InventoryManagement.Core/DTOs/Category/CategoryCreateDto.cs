namespace InventoryManagement.Core.DTOs.Category
{
    public class CategoryCreateDto
    {
        public string Name { get; set; } = string.Empty;



        #region Relationship - Affiliated with the upper class
        public int CompanyId { get; set; }
        #endregion
    }
}
