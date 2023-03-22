using InventoryManagement.Core.DTOs.Category;
using InventoryManagement.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : CustomBaseController
    {
        private readonly ICategoryServiceWithDto _service;

        public CategoryController(ICategoryServiceWithDto service)
        {
            _service = service;
        }


        [HttpGet("{companyId}/{page}/{pageSize}")]
        //[Authorize(Roles = "SuperAdminRole", Policy = "company#get")]
        public async Task<IActionResult> GetCategoryList(int companyId, int page, int pageSize)
        {
            return CreateActionResult( await _service.GetCategoryList(companyId, page, pageSize));
        }


       
        [HttpGet("{companyId}")]
       // [Authorize(Roles = "SuperAdminRole", Policy = "company#get")]
        public async Task<IActionResult> GetCategoryList(int companyId)
        {
            return CreateActionResult(await _service.GetCategoryList(companyId));
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoryAllList()
        {
            return CreateActionResult(await _service.GetCategoryAllList());
        }

        [HttpPost]
        //[Authorize(Roles = "SuperAdminRole", Policy = "company#create")]
        public async Task<IActionResult> AddAsync([FromBody] CategoryCreateDto dto)
        {
            return CreateActionResult(await _service.AddAsync(dto));
        }

        [HttpPut]
        //[Authorize(Roles = "SuperAdminRole", Policy = "company#update")]
        public async Task<IActionResult> UpdateAsync([FromBody] CategoryUpdateDto dto)
        {
            return CreateActionResult(await _service.UpdateAsync(dto));
        }



        [HttpDelete("{id}")]
        //[Authorize(Roles = "SuperAdminRole", Policy = "company#delete")]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            return CreateActionResult(await _service.RemoveAsync(id));
        }


    }
}
