using InventoryManagement.Core.DTOs;
using InventoryManagement.Core.DTOs.Model;
using InventoryManagement.Core.Models;

namespace InventoryManagement.Core.Services
{
    public interface IModelServiceWithDto : IServiceWithDto<Model, ModelDto>
    {
        Task<CustomResponseDto<NoContent>> UpdateAsync(ModelUpdateDto dto);
        Task<CustomResponseDto<ModelDto>> AddAsync(ModelCreateDto dto);
    }
}
