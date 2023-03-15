namespace InventoryManagement.Core.Models
{
    public class Model : BaseEntity
    {
        public string Name { get; set; } = string.Empty;




        #region Relationship - Affiliated with upper class
        public int BrandId { get; set; }
        public Brand Brand { get; set; } = default!;
        #endregion
    }
}
