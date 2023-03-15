using AutoMapper;
using InventoryManagement.Core.DTOs;
using InventoryManagement.Core.DTOs.Model;
using InventoryManagement.Core.Models;
using InventoryManagement.Core.Repositories;
using InventoryManagement.Core.Services;
using InventoryManagement.Core.UnitOfWork;
using Microsoft.AspNetCore.Http;

namespace InventoryManagement.Services.Services
{
    public class ModelServiceWithDto : ServiceWithDto<Model, ModelDto>, IModelServiceWithDto
    {
        private readonly IModelRepository _repository;
        public ModelServiceWithDto(IGenericRepository<Model> repository, IUnitOfWork unitOfWork, IMapper mapper, IModelRepository modelRepository) : base(repository, unitOfWork, mapper)
        {
            _repository = modelRepository;
        }

        public async Task<CustomResponseDto<ModelDto>> AddAsync(ModelCreateDto dto)
        {
            var newEntity = _mapper.Map<Model>(dto);
            await _repository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();

            var newDto = _mapper.Map<ModelDto>(newEntity);
            return CustomResponseDto<ModelDto>.Success(StatusCodes.Status200OK, newDto);
        }

        public async Task<CustomResponseDto<NoContent>> UpdateAsync(ModelUpdateDto dto)
        {
            var entity = _mapper.Map<Model>(dto);
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContent>.Success(StatusCodes.Status204NoContent);
        }
    }
}
