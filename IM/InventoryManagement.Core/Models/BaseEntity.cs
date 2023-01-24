namespace InventoryManagement.Core.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public int BusinessCode { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
