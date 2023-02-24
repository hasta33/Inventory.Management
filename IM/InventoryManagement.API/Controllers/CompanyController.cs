using InventoryManagement.Core.DTOs.Company;
using InventoryManagement.Core.Services;
using Microsoft.AspNetCore.Authorization;
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

        
        [HttpGet("{page}/{pageSize}/{businessCode}")]
        public async Task<IActionResult> GetCompanyList(int page, int pageSize, int businessCode)
        {
            return CreateActionResult(await _service.GetCompanyList(page, pageSize, businessCode));
        }


        
        [HttpGet("{businessCode}")]
        [Authorize(Roles = "SuperAdminRole", Policy = "company#get")]
        public async Task<IActionResult> GetCompanyWithCategoryListAsync(int businessCode)
        {
            return CreateActionResult(await _service.GetCompanyWithCategoryListAsync(businessCode));
        }



        [HttpPost]
        [Authorize("company#create")]
        public async Task<IActionResult> AddAsync([FromBody] CompanyCreateDto dto)
        {
            return CreateActionResult(await _service.AddAsync(dto));
        }



        [HttpPut]
        [Authorize("company#update")]
        public async Task<IActionResult> UpdateAsync([FromBody] CompanyUpdateDto dto)
        {
            return CreateActionResult(await _service.UpdateAsync(dto));
        }



        [HttpDelete("{id}")]
        [Authorize("company#delete")]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            return CreateActionResult(await _service.RemoveAsync(id));
        }



        //[HttpPost("SaveAll")]
        //public async Task<IActionResult> SaveAll(List<CompanyDto> dtos)
        //{
        //    return CreateActionResult(await _service.AddRangeAsync(dtos));
        //}

        //[HttpDelete("RemoveAll")]
        //public async Task<IActionResult> RemoveAll(List<int> ids)
        //{
        //    return CreateActionResult(await _service.RemoveRangeAsync(ids));
        //}

    }
}
