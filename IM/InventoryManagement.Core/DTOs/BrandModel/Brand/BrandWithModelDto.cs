using InventoryManagement.Core.DTOs.BrandModel.Model;

namespace InventoryManagement.Core.DTOs.BrandModel.Brand
{
    public class BrandWithModelDto : BrandDto
    {
        public List<ModelDto> ModelDtos { get; set; }
    }
}
