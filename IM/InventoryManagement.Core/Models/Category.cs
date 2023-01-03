namespace InventoryManagement.Core.Models
{
    public class Category : BaseEntity
    {
        public string? Name { get; set; }




        //public ICollection<CategorySub> ? CategorySubs { get; set; }
       
        public IEnumerable<CategorySub>? CategorySubs { get; set; }
    }
}
