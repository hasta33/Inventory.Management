using AutoMapper;
using InventoryManagement.Core.DTOs.Company;
using InventoryManagement.Core.Models;
using InventoryManagement.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : CustomBaseController
    {
        private readonly IServiceWithDto<Company, CompanyDto> _service;
        private readonly IMapper _mapper;

        public CompanyController(IServiceWithDto<Company, CompanyDto> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return CreateActionResult(await _service.GetAllAsync());
        }


        [HttpPost]
        public async Task<IActionResult> AddAsync(CompanyCreateDto dto)
        {
            var result = _mapper.Map<CompanyDto>(dto);
            return CreateActionResult(await _service.AddAsync(result));
        }


        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] CompanyUpdateDto dto)
        {
            var result = _mapper.Map<CompanyDto>(dto);
            return CreateActionResult(await _service.UpdateAsync(result));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            return CreateActionResult(await _service.RemoveAsync(id));
        }


        [HttpPost("SaveAll")]
        public async Task<IActionResult> SaveAll(List<CompanyCreateDto> dtos)
        {
            var result = _mapper.Map<List<CompanyDto>>(dtos);
            return CreateActionResult(await _service.AddRangeAsync(result));
        }

        [HttpDelete("RemoveAll")]
        public async Task<IActionResult> RemoveAll(List<int> ids)
        {
            return CreateActionResult(await _service.RemoveRangeAsync(ids));
        }





    }
}
