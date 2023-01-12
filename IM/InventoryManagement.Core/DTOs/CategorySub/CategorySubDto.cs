namespace InventoryManagement.Core.DTOs.CategorySub
{
    public class CategorySubDto : BaseDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

        
        //Relationship
        public int CategoryId { get; set; }
    }
}
