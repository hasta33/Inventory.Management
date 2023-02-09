namespace InventoryManagement.Core.Models
{
    public class CategorySub : BaseEntity
    {
        public string Name { get; set; } = string.Empty;




        #region Relationship - Affiliated with the upper class
        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!; //sonradan eklendi
        #endregion
    }
}
