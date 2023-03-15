namespace InventoryManagement.Core.DTOs.Brand
{
    public class BrandCreateDto
    {
        public string Name { get; set; }




        #region Relationship - Affiliated with the upper class
        public int CompanyId { get; set; }
        #endregion
    }
}
