using InventoryManagement.Core.DTOs.Model;

namespace InventoryManagement.Core.DTOs.Brand
{
    public class BrandDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int BusinessCode { get; set; }
        public int TotalCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }




        #region Relationship - Affiliated with the upper class
        public int CompanyId { get; set; }
        #endregion


        #region Relationship - SubClass
        public List<ModelDto>? Models { get; set; }
        #endregion
    }
}
