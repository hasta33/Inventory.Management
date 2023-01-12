namespace InventoryManagement.Core.Models
{
    public class CategorySub : BaseEntity
    {
        public string? Name { get; set; }
        public string ? Description { get; set; }





        public int ? CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
