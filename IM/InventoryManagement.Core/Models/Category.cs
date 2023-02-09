namespace InventoryManagement.Core.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = string.Empty;




        #region Relationship - Affiliated with the upper class
        public int CompanyId { get; set; }
        public Company Company { get; set; } = default!; //sonradan eklendi
        #endregion


        #region Relationship - Fetch Subclasses
        public ICollection<CategorySub> CategorySubs { get; set; } = new List<CategorySub>();
        #endregion
    }
}
