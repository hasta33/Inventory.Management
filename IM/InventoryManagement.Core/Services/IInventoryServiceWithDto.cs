using InventoryManagement.Core.DTOs;
using InventoryManagement.Core.DTOs.Inventory;
using InventoryManagement.Core.Models;

namespace InventoryManagement.Core.Services
{
    public interface IInventoryServiceWithDto : IServiceWithDto<Inventory, InventoryDto>
    {
        //Task<CustomResponseDto<List<InventoryDto>>> GetInventoryList(int companyId, int page, int pageSize);
        Task<CustomResponseDto<List<InventoryDto>>> GetInventoryList(FilteringParameters parameters, int page, int pageSize);


        Task<CustomResponseDto<InventoryDto>> AddAsync(InventoryCreateDto dto);
        Task<CustomResponseDto<NoContent>> UpdateAsync(InventoryUpdateDto dto);
        Task<CustomResponseDto<NoContent>> UpdateAsyncEmbezzled(EmbezzledUpdateDto dto);
    }


}
