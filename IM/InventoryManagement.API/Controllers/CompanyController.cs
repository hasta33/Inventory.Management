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



        [HttpGet("{page}/{pageSize}")]
        //[Authorize(Roles = "CompanyRole", Policy = "company#get")]
        public async Task<IActionResult> GetCompanyAllList(int page, int pageSize)
        {
            return CreateActionResult(await _service.GetCompanyAllList(page, pageSize));
        }
        

        [HttpGet("{companyId}/{page}/{pageSize}")]
        //[Authorize(Roles = "CompanyRole", Policy = "company#get")]
        public async Task<IActionResult> GetCompanyListWithSubTables(int companyId, int page, int pageSize)
        {
            return CreateActionResult(await _service.GetCompanyListWithSubTables(companyId, page, pageSize));
        }



        [HttpPost]
        //[Authorize(Roles = "CompanyRole", Policy = "company#create")]
        public async Task<IActionResult> AddAsync([FromBody] CompanyCreateDto dto)
        {
            return CreateActionResult(await _service.AddAsync(dto));
        }



        [HttpPut]
        //[Authorize(Roles = "CompanyRole", Policy = "company#update")]
        public async Task<IActionResult> UpdateAsync([FromBody] CompanyUpdateDto dto)
        {
            return CreateActionResult(await _service.UpdateAsync(dto));
        }



        [HttpDelete("{id}")]
       // [Authorize(Roles = "CompanyRole", Policy = "company#delete")]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            return CreateActionResult(await _service.RemoveAsync(id));
        }


        ////BU ALAN ŞİMDİLİK KULLANILMADI, FRONTEND TARAFINDA KULLANILACAK
        //[HttpGet("{businessCode}")]
        ////[Authorize(Roles = "SuperAdminRole", Policy = "company#get")]
        //public async Task<IActionResult> GetCompanyWithCategoryListAsync(int businessCode)
        //{
        //    return CreateActionResult(await _service.GetCompanyWithCategoryListAsync(businessCode));
        //}




        //[HttpPost("SaveAll")]
        //[Authorize(Roles = "CompanyRole", Policy = "company#create")]
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
