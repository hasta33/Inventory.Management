namespace InventoryManagement.Core.DTOs.BrandModel.Model
{
    public class ModelUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BusinessCode { get; set; }


        //Relationship
        public int BrandId { get; set; }
    }
}
