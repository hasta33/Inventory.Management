using AutoMapper;
using InventoryManagement.Core.DTOs;
using InventoryManagement.Core.DTOs.Inventory;
using InventoryManagement.Core.DTOs.InventoryMovement;
using InventoryManagement.Core.Models;
using InventoryManagement.Core.Repositories;
using InventoryManagement.Core.Services;
using InventoryManagement.Core.UnitOfWork;
using Microsoft.AspNetCore.Http;

namespace InventoryManagement.Services.Services
{
    public class InventoryServiceWithDto : ServiceWithDto<Inventory, InventoryDto>, IInventoryServiceWithDto
    {
        private readonly IInventoryRepository _inventoryRepository;
        //private readonly IInventoryMovementRepository _inventoryMovementRepository;
        public InventoryServiceWithDto(IGenericRepository<Inventory> repository, IUnitOfWork unitOfWork, IMapper mapper, IInventoryRepository inventoryRepository) : base(repository, unitOfWork, mapper)
        {
            _inventoryRepository = inventoryRepository;
            //_inventoryMovementRepository = inventoryMovementRepository;
        }

        public async Task<CustomResponseDto<InventoryDto>> AddAsync(InventoryCreateDto dto)
        {
            var newEntity = _mapper.Map<Inventory>(dto);
            await _inventoryRepository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();

            ////Movement add
            //var newMovementEntity = _mapper.Map<InventoryMovement>(dto);
            //await _inventoryMovementRepository.AddAsync(newMovementEntity);
            //await _unitOfWork.CommitAsync();
            ////movement add end

            var newDto = _mapper.Map<InventoryDto>(newEntity);
            return CustomResponseDto<InventoryDto>.Success(StatusCodes.Status200OK, newDto);
        }

        //public async Task<CustomResponseDto<List<InventoryDto>>> GetInventoryList(int companyId, int page, int pageSize)
        public async Task<CustomResponseDto<List<InventoryDto>>> GetInventoryList(FilteringParameters parameters, int page, int pageSize)
        {
            var inventory = await _inventoryRepository.GetInventoryList(parameters, page, pageSize);
            var inventoryDto = _mapper.Map<List<InventoryDto>>(inventory);
            return CustomResponseDto<List<InventoryDto>>.Success(StatusCodes.Status200OK, inventoryDto);
        }

        public async Task<CustomResponseDto<NoContent>> UpdateAsync(InventoryUpdateDto dto)
        {
            var entity = _mapper.Map<Inventory>(dto);
            _inventoryRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContent>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponseDto<NoContent>> UpdateAsyncEmbezzled(EmbezzledUpdateDto dto)
        {
            var entity = _mapper.Map<Inventory>(dto);
            _inventoryRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContent>.Success(StatusCodes.Status204NoContent);
        }
    }
}
