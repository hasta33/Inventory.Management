using AutoMapper;
using InventoryManagement.Core.DTOs;
using InventoryManagement.Core.DTOs.Inventory;
using InventoryManagement.Core.Models;
using InventoryManagement.Core.Repositories;
using InventoryManagement.Core.Services;
using InventoryManagement.Core.UnitOfWork;
using InventoryManagement.Shared.RabbitMQ.Commands;
using MassTransit;
using Microsoft.AspNetCore.Http;

namespace InventoryManagement.Services.Services
{
    public class InventoryServiceWithDto : ServiceWithDto<Inventory, InventoryDto>, IInventoryServiceWithDto
    {
        private readonly IInventoryRepository _inventoryRepository;
        //private readonly ISendEndpointProvider _sendEndpointProvider; //command type

        //private readonly IInventoryMovementRepository _inventoryMovementRepository;
        //public InventoryServiceWithDto(IGenericRepository<Inventory> repository, IUnitOfWork unitOfWork, IMapper mapper, ISendEndpointProvider sendEndpointProvider, IInventoryRepository inventoryRepository) : base(repository, unitOfWork, mapper)
        //{
        //    _inventoryRepository = inventoryRepository;
        //    //_sendEndpointProvider = sendEndpointProvider;
        //}
        public InventoryServiceWithDto(IGenericRepository<Inventory> repository, IUnitOfWork unitOfWork, IMapper mapper, IInventoryRepository inventoryRepository) : base(repository, unitOfWork, mapper)
        {
            _inventoryRepository = inventoryRepository;
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

        public async Task<CustomResponseDto<InventoryDto>> InventoryEmbezzled(InventoryEmbezzledDto dto)
        {
            var entity = _mapper.Map<Inventory>(dto);
            _inventoryRepository.Update(entity);
            await _unitOfWork.CommitAsync();


            //Publisher
            //rabbitmq notification
            /*await _publishEndpoint.Publish<InventoryEmbezzledMessageCommand>(new
            {
                dto.Id,
                dto.Responsible,
                dto.Barcode,
                dto.SerialNumber
            });*/

            //////Command send
            //var sendCommandEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:inventory-embezzled"));
            //var embezzled = new InventoryEmbezzledMessageCommand();
            //embezzled.SerialNumber = dto.SerialNumber;
            //embezzled.Imei = dto.Imei;
            //embezzled.Responsible = dto.Responsible;
            //embezzled.Barcode = dto.Barcode;
            //embezzled.Mac = dto.Mac;
            //embezzled.Id = dto.Id;

            //await sendCommandEndpoint.Send<InventoryEmbezzledMessageCommand>(embezzled);

            return CustomResponseDto<InventoryDto>.Success(StatusCodes.Status204NoContent);

        }

        public async Task<CustomResponseDto<NoContent>> UpdateAsync(InventoryUpdateDto dto)
        {
            var entity = _mapper.Map<Inventory>(dto);
            _inventoryRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContent>.Success(StatusCodes.Status204NoContent);
        }


    }
}
