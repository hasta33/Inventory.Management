using AutoMapper;
using InventoryManagement.Core.DTOs;
using InventoryManagement.Core.DTOs.CategorySub;
using InventoryManagement.Core.Models;
using InventoryManagement.Core.Repositories;
using InventoryManagement.Core.Services;
using InventoryManagement.Core.UnitOfWork;
using Microsoft.AspNetCore.Http;

namespace InventoryManagement.Services.Services
{
    public class CategorySubServiceWithDto : ServiceWithDto<CategorySub, CategorySubDto>, ICategorySubServiceWithDto
    {
        private readonly ICategorySubRepository _repository;
        public CategorySubServiceWithDto(IGenericRepository<CategorySub> repository, IUnitOfWork unitOfWork, IMapper mapper, ICategorySubRepository categorySubRepository) : base(repository, unitOfWork, mapper)
        {
            _repository = categorySubRepository;
        }

        public async Task<CustomResponseDto<CategorySubDto>> AddAsync(CategorySubCreateDto dto)
        {
            var newEntity = _mapper.Map<CategorySub>(dto);
            await _repository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();

            var newDto = _mapper.Map<CategorySubDto>(newEntity);
            return CustomResponseDto<CategorySubDto>.Success(StatusCodes.Status200OK, newDto);
        }

        public async Task<CustomResponseDto<NoContent>> UpdateAsync(CategorySubUpdateDto dto)
        {
            var entity = _mapper.Map<CategorySub>(dto);
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContent>.Success(StatusCodes.Status204NoContent);
        }
    }
}
