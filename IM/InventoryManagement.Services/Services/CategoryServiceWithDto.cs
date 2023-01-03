using AutoMapper;
using InventoryManagement.Core.DTOs;
using InventoryManagement.Core.DTOs.Category;
using InventoryManagement.Core.Models;
using InventoryManagement.Core.Repositories;
using InventoryManagement.Core.Services;
using InventoryManagement.Core.UnitOfWork;
using Microsoft.AspNetCore.Http;

namespace InventoryManagement.Services.Services
{
    public class CategoryServiceWithDto : ServiceWithDto<Category, CategoryDto>, ICategoryServiceWithDto
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryServiceWithDto(IGenericRepository<Category> repository, IUnitOfWork unitOfWork, IMapper mapper, ICategoryRepository categoryRepository) : base(repository, unitOfWork, mapper)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CustomResponseDto<CategoryDto>> AddAsync(CategoryCreateDto dto)
        {
            var newEntity = _mapper.Map<Category>(dto);
            await _categoryRepository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();

            var newDto = _mapper.Map<CategoryDto>(dto);
            return CustomResponseDto<CategoryDto>.Success(StatusCodes.Status200OK, newDto);
        }

        public async Task<CustomResponseDto<List<CategoryWithCategorySubDto>>> GetCategoryByIdWithSubCategory(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdWithSubCategory(id);

            var categoryDto = _mapper.Map<List<CategoryWithCategorySubDto>>(category);
            return CustomResponseDto<List<CategoryWithCategorySubDto>>.Success(StatusCodes.Status200OK, categoryDto);
        }

        public async Task<CustomResponseDto<NoContent>> UpdateAsync(CategoryUpdateDto dto)
        {
            var entity = _mapper.Map<Category>(dto);
            _categoryRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContent>.Success(StatusCodes.Status204NoContent);
        }
    }
}
