using AutoMapper;
using InventoryManagement.Core.DTOs;
using InventoryManagement.Core.DTOs.Brand;
using InventoryManagement.Core.Models;
using InventoryManagement.Core.Repositories;
using InventoryManagement.Core.Services;
using InventoryManagement.Core.UnitOfWork;
using Microsoft.AspNetCore.Http;

namespace InventoryManagement.Services.Services
{
    public class BrandServiceWithDto : ServiceWithDto<Brand, BrandDto>, IBrandServiceWithDto
    {
        private readonly IBrandRepository _brandRepository;

        public BrandServiceWithDto(IGenericRepository<Brand> repository, IUnitOfWork unitOfWork, IMapper mapper, IBrandRepository brandRepository) : base(repository, unitOfWork, mapper)
        {
            _brandRepository = brandRepository;
        }

        public async Task<CustomResponseDto<BrandDto>> AddAsync(BrandCreateDto dto)
        {
            var newEntity = _mapper.Map<Brand>(dto);
            await _brandRepository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();

            var newDto = _mapper.Map<BrandDto>(newEntity);
            return CustomResponseDto<BrandDto>.Success(StatusCodes.Status200OK, newDto);
        }

        public async Task<CustomResponseDto<List<BrandDto>>> GetBrandAllList()
        {
            var brand = await _brandRepository.GetBrandAllList();
            var brandDto = _mapper.Map<List<BrandDto>>(brand);
            return CustomResponseDto<List<BrandDto>>.Success(StatusCodes.Status200OK, brandDto);
        }

        public async Task<CustomResponseDto<List<BrandDto>>> GetBrandList(int companyId, int page, int pageSize)
        {
            var brand = await _brandRepository.GetBrandList(companyId, page, pageSize);
            var brandDto = _mapper.Map<List<BrandDto>>(brand);
            return CustomResponseDto<List<BrandDto>>.Success(StatusCodes.Status200OK, brandDto);
        }

        public async Task<CustomResponseDto<List<BrandDto>>> GetBrandList(int companyId)
        {
            var brand = await _brandRepository.GetBrandList(companyId);
            var brandDto = _mapper.Map<List<BrandDto>>(brand);
            return CustomResponseDto<List<BrandDto>>.Success(StatusCodes.Status200OK, brandDto);
        }

        public async Task<CustomResponseDto<NoContent>> UpdateAsync(BrandUpdateDto dto)
        {
            var entity = _mapper.Map<Brand>(dto);
            _brandRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContent>.Success(StatusCodes.Status204NoContent);
        }
    }
}
