using AutoMapper;
using InventoryManagement.Core.DTOs;
using InventoryManagement.Core.DTOs.Company;
using InventoryManagement.Core.Models;
using InventoryManagement.Core.Repositories;
using InventoryManagement.Core.Services.Entity;
using InventoryManagement.Core.UnitOfWork;

/* Entity şeklinde yapmak için */
namespace InventoryManagement.Services.Services.Entity
{
    public class CompanyService : Service<Company>, ICompanyService
    {
        private readonly ICompanyRepository _repository;
        private readonly IMapper _mapper;

        public CompanyService(IGenericRepository<Company> repository, IUnitOfWork unitOfWork, ICompanyRepository companyRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _repository = companyRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<List<CompanyDto>>> GetCompanies()
        {
            var companies = await _repository.GetCompanies();
            var companiesDto = _mapper.Map<List<CompanyDto>>(companies);
            return CustomResponseDto<List<CompanyDto>>.Success(200, companiesDto);
        }

        public async Task<CustomResponseDto<List<CompanyDto>>> GetCompaniesById(int id)
        {
            var company = await _repository.GetByIdAsync(id);
            var companyDto = _mapper.Map<List<CompanyDto>>(company);
            return CustomResponseDto<List<CompanyDto>>.Success(200, companyDto);
        }

        public async Task<CustomResponseDto<List<CompanyDto>>> GetCompaniesPageList(int page, int pageSize)
        {
            var companyList = await _repository.GetCompaniesList(page, pageSize);
            return CustomResponseDto<List<CompanyDto>>.Success(200, companyList);
        }
    }
}
