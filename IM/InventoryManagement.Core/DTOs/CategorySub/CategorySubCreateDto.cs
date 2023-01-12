namespace InventoryManagement.Core.DTOs.CategorySub
{
    public class CategorySubCreateDto //: BaseDto
    {
        public string ? Name { get; set; }
        public string? Description { get; set; }
        public int BusinessCode { get; set; }


        //Relationship
        public int CategoryId { get; set; }
    }
}
