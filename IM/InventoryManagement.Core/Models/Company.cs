namespace InventoryManagement.Core.Models
{
    public class Company : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;



        #region Relationship
        public ICollection<Category> Categories { get; set; } = new List<Category>();
        #endregion
    }
}
