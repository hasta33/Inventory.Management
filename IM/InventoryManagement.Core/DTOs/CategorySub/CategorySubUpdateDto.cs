namespace InventoryManagement.Core.DTOs.CategorySub
{
    public class CategorySubUpdateDto //: BaseDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int BusinessCode { get; set; }



        //Relationship
        public int CategoryId { get; set; }
    }
}
