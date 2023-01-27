namespace InventoryManagement.Core.DTOs
{
    public class BaseDto
    {
        public int Id { get; set; }
        public int BusinessCode { get; set; }
        public int TotalCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
