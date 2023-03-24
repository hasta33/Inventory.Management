using AutoMapper;
using InventoryManagement.Core.DTOs;
using InventoryManagement.Core.DTOs.InventoryMovement;
using InventoryManagement.Core.Models;
using InventoryManagement.Core.Repositories;
using InventoryManagement.Core.Services;
using InventoryManagement.Core.UnitOfWork;
using Microsoft.AspNetCore.Http;

namespace InventoryManagement.Services.Services
{
    public class InventoryMovementServiceWithDto : ServiceWithDto<InventoryMovement, InventoryMovementDto>, IInventoryMovementServiceWithDto
    {
        public readonly IInventoryMovementRepository _inventoryMovementRepository;

        public InventoryMovementServiceWithDto(IGenericRepository<InventoryMovement> repository, IUnitOfWork unitOfWork, IMapper mapper, IInventoryMovementRepository inventoryMovemenetRespository) : base(repository, unitOfWork, mapper)
        {
            _inventoryMovementRepository = inventoryMovemenetRespository;
        }

        public async Task<CustomResponseDto<InventoryMovementDto>> AddAsync(InventoryMovementCreateDto dto)
        {
            var newEntity = _mapper.Map<InventoryMovement>(dto);
            await _inventoryMovementRepository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();

            var newDto = _mapper.Map<InventoryMovementDto>(newEntity);
            return CustomResponseDto<InventoryMovementDto>.Success(StatusCodes.Status200OK, newDto);
        }

        public async Task<CustomResponseDto<List<InventoryMovementDto>>> GetInventoryMovement(int inventoryId)
        {
            var inventory = await _inventoryMovementRepository.GetInventoryMovements(inventoryId);
            var inventoryDto = _mapper.Map<List<InventoryMovementDto>>(inventory);
            return CustomResponseDto<List<InventoryMovementDto>>.Success(StatusCodes.Status200OK, inventoryDto);
        }
    }
}
