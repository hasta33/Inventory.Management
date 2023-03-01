using AutoMapper;
using InventoryManagement.Core.DTOs;
using InventoryManagement.Core.DTOs.Company;
using InventoryManagement.Core.Models;
using InventoryManagement.Core.Repositories;
using InventoryManagement.Core.Services;
using InventoryManagement.Core.UnitOfWork;
using Microsoft.AspNetCore.Http;

namespace InventoryManagement.Services.Services
{
    public class CompanyServiceWithDto : ServiceWithDto<Company, CompanyDto>, ICompanyServiceWithDto
    {
        private readonly ICompanyRepository _companyRepository;
        public CompanyServiceWithDto(IGenericRepository<Company> repository, IUnitOfWork unitOfWork, IMapper mapper, ICompanyRepository companyRepository) : base(repository, unitOfWork, mapper)
        {
            _companyRepository = companyRepository;
        }

        public async Task<CustomResponseDto<CompanyDto>> AddAsync(CompanyCreateDto dto)
        {
            var newEntity = _mapper.Map<Company>(dto);
            await _companyRepository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();

            var newDto = _mapper.Map<CompanyDto>(newEntity);
            return CustomResponseDto<CompanyDto>.Success(StatusCodes.Status200OK, newDto);

        }

        public async Task<CustomResponseDto<NoContent>> UpdateAsync(CompanyUpdateDto dto)
        {
            var entity = _mapper.Map<Company>(dto);
            _companyRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContent>.Success(StatusCodes.Status204NoContent);
        }


        public async Task<CustomResponseDto<List<CompanyDto>>> GetCompanyListWithSubTables(int companyId, int page, int pageSize)
        {
            var company = await _companyRepository.GetCompanyListWithSubTables(companyId, page, pageSize);
            var companyDto = _mapper.Map<List<CompanyDto>>(company);
            return CustomResponseDto<List<CompanyDto>>.Success(StatusCodes.Status200OK, companyDto);
        }

        public async Task<CustomResponseDto<List<CompanyAllDto>>> GetCompanyAllList(int page, int pageSize)
        {
            var company = await _companyRepository.GetCompanyAllList(page, pageSize);
            var companyDto = _mapper.Map<List<CompanyAllDto>>(company);
            return CustomResponseDto<List<CompanyAllDto>>.Success(StatusCodes.Status200OK, companyDto);
        }

        //public async Task<CustomResponseDto<List<CompanyDto>>> GetCompanyWithCategoryListAsync(int businessCode)
        //{
        //    var result = await _companyRepository.GetCompanyWithCategoryListAsync(businessCode);
        //    var resultDto = _mapper.Map<List<CompanyDto>>(result);
        //    return CustomResponseDto<List<CompanyDto>>.Success(StatusCodes.Status200OK, resultDto);
        //}
    }
}
