using AutoMapper;
using InventoryManagement.Core.DTOs;
using InventoryManagement.Core.DTOs.Company;
using InventoryManagement.Core.Models;
using InventoryManagement.Core.Services.Entity;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : CustomBaseController
    {

        private readonly IMapper _mapper;
        private readonly IService<Company> _service;
        private readonly ICompanyService _companyService;
        private readonly ILogger<CompanyController> _logger;

        public CompanyController(IMapper mapper, IService<Company> service, ICompanyService companyService, ILogger<CompanyController> logger)
        {
            _mapper = mapper;
            _service = service;
            _companyService = companyService;
            _logger = logger;
        }


        [HttpGet("{page}/{pageSize}")]
        public async Task<IActionResult> CompaniesList(int page, int pageSize)
        {
            return CreateActionResult(await _companyService.GetCompaniesPageList(page, pageSize));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompainesById(int id)
        {
            var company = await _service.GetByIdAsync(id);
            var companyDto = _mapper.Map<CompanyDto>(company);
            return CreateActionResult(CustomResponseDto<CompanyDto>.Success(200, companyDto));
        }


        [HttpPost]
        public async Task<IActionResult> Create(CompanyDto createCompany)
        {
            var company = await _service.AddAsync(_mapper.Map<Company>(createCompany));
            var companyDto = _mapper.Map<CompanyDto>(company);
            return CreateActionResult(CustomResponseDto<CompanyDto>.Success(201, companyDto));
        }




        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CompanyUpdateDto updateCompany)
        {
            await _service.UpdateAsync(_mapper.Map<Company>(updateCompany));
            return CreateActionResult(CustomResponseDto<CompanyUpdateDto>.Success(204));


            //JsonSerializerOptions options = new()
            //{
            //    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            //};
            //var serialized = JsonSerializer.Serialize(updateCompany, options);
            //Console.WriteLine(serialized);
            //return CreateActionResult(CustomResponseDto<CompanyUpdateDto>.Success(204));


            //JsonSerializerOptions options = new()
            //{
            //    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            //};

            //var serialized = JsonSerializer.Serialize(updateCompany, options);
            //return CreateActionResult(CustomResponseDto<CompanyUpdateDto>.Success(204));


            //burası calısıyor
            //// Get existing entity by id (Example)
            //var _company = await _service.GetByIdAsync(updateCompany.Id);
            //// Map source to destination
            //_mapper.Map<CompanyUpdateDto, Company>(updateCompany, _company);
            //await _service.UpdateAsync(_company);
            //return CreateActionResult(CustomResponseDto<CompanyUpdateDto>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var result = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(result);

            return CreateActionResult(CustomResponseDto<CompanyDto>.Success(200));
        }

    }
}
