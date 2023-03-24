using InventoryManagement.Core.DTOs;
using InventoryManagement.Core.DTOs.InventoryMovement;
using InventoryManagement.Core.Models;

namespace InventoryManagement.Core.Services
{
    public interface IInventoryMovementServiceWithDto : IServiceWithDto<InventoryMovement, InventoryMovementDto>
    {
        Task<CustomResponseDto<List<InventoryMovementDto>>> GetInventoryMovement(int inventoryId);
        Task<CustomResponseDto<InventoryMovementDto>> AddAsync(InventoryMovementCreateDto dto);
    }
}
