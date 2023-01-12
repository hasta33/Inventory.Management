namespace InventoryManagement.Core.Models
{
    public class Model : BaseEntity
    {
        public string ? Name { get; set; }



        //Relationship
        public int ? BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
