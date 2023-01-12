namespace InventoryManagement.Core.Models
{
    public class Category : BaseEntity
    {
        public string? Name { get; set; }




        public int? CompanyId { get; set; }
        public Company Company { get; set; }


        public ICollection<CategorySub> CategorySubs { get; set; }
    }
}
