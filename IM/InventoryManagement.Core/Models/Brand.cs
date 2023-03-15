namespace InventoryManagement.Core.Models
{
    public class Brand: BaseEntity
    {
        public string Name {  get; set; } = string.Empty;





        #region RelationShip - Affiliated with the upper class
        public int CompanyId { get; set; }
        public Company Company { get; set; } = default!;
        #endregion



        #region Relationship - Fetch Subclasses
        public ICollection<Model> Models { get; set; } = new List<Model>();
        #endregion
    }
}