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
    /* CustomRespondeDto şeklinde yapmak için */
    public class CategoryServiceWithDto : ServiceWithDto<Category, CategoryDto>, ICategoryServiceWithDto
    {
        private readonly ICategoryRepository _repository;
        public CategoryServiceWithDto(IGenericRepository<Category> repository, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper) : base(repository, unitOfWork, mapper)
        {
            _repository = categoryRepository;
        }

        public async Task<CustomResponseDto<CategoryDto>> AddAsync(CategoryCreateDto dto)
        {
            var newEntity = _mapper.Map<Category>(dto);
            await _repository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();

            var newDto = _mapper.Map<CategoryDto>(newEntity);
            return CustomResponseDto<CategoryDto>.Success(StatusCodes.Status200OK, newDto);
        }

        public async Task<CustomResponseDto<List<CategoryDto>>> GetCategories()
        {
            var categories = await _repository.GetCategories();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);
            return CustomResponseDto<List<CategoryDto>>.Success(200, categoriesDto);
        }

        public async Task<CustomResponseDto<List<CategoryDto>>> GetCategoriesById(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            var categoryDto = _mapper.Map<List<CategoryDto>>(category);
            return CustomResponseDto<List<CategoryDto>>.Success(200, categoryDto);
        }

        public async Task<CustomResponseDto<List<CategoryDto>>> GetCategoriesPageList(int page, int pageSize)
        {
            var categoryList = await _repository.GetCategoriesList(page, pageSize);
            return CustomResponseDto<List<CategoryDto>>.Success(200, categoryList);
        }
        
        public async Task<CustomResponseDto<NoContent>> UpdateAsync(CategoryUpdateDto dto)
        {
            var entity = _mapper.Map<Category>(dto);
            _repository.Update(entity);

            await _unitOfWork.CommitAsync();

            return CustomResponseDto<NoContent>.Success(StatusCodes.Status204NoContent);
        }
    }
}
