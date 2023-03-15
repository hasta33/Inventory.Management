namespace InventoryManagement.Core.DTOs.Model
{
    public class ModelDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set;}




        #region Relationship - Affiliated with the upper class
        public int BrandId { get; set; }
        #endregion 
    }
}
