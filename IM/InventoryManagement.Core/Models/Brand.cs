namespace InventoryManagement.Core.Models
{
    public class Brand : BaseEntity
    {
        public string? Name { get; set; }



        //Relationship
        public ICollection<Model> Models { get; set; }
    }
}
