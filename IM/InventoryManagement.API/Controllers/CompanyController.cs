using InventoryManagement.Core.DTOs.Company;
using InventoryManagement.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : CustomBaseController
    {
        private readonly ICompanyServiceWithDto _service;

        public CompanyController(ICompanyServiceWithDto service)
        {
            _service = service;
        }


        [HttpGet("{page}/{pageSize}")]
        public async Task<IActionResult> GetCompanyList(int page, int pageSize)
        {
            return CreateActionResult(await _service.GetCompanyList(page, pageSize));
        }


        [HttpPost]
        public async Task<IActionResult> AddAsync(CompanyCreateDto dto)
        {
            return CreateActionResult(await _service.AddAsync(dto));
        }


        [HttpPut]
        public async Task<IActionResult> UpdateAsync(CompanyUpdateDto dto)
        {
            return CreateActionResult(await _service.UpdateAsync(dto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            return CreateActionResult(await _service.RemoveAsync(id));
        }



        [HttpPost("SaveAll")]
        public async Task<IActionResult> SaveAll(List<CompanyDto> dtos)
        {
            return CreateActionResult(await _service.AddRangeAsync(dtos));
        }

        [HttpDelete("RemoveAll")]
        public async Task<IActionResult> RemoveAll(List<int> ids)
        {
            return CreateActionResult(await _service.RemoveRangeAsync(ids));
        }

    }
}
