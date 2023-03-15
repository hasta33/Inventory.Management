namespace InventoryManagement.Core.DTOs.Model
{
    public class ModelCreateDto
    {
        public string Name { get; set; } = string.Empty;


        #region Relationship - Affiliated with the upper class
        public int BrandId { get; set; }
        #endregion
    }
}
