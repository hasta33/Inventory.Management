using InventoryManagement.Core.DTOs.Category;
using InventoryManagement.Core.Services;
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

        
        
        
        [HttpGet("{businessCode}")]
        public async Task<IActionResult> GetCategoryList(int businessCode)
        {
            return CreateActionResult(await _service.GetCategoryList(businessCode));
        }


        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CategoryCreateDto dto)
        {
            return CreateActionResult(await _service.AddAsync(dto));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] CategoryUpdateDto dto)
        {
            return CreateActionResult(await _service.UpdateAsync(dto));
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            return CreateActionResult(await _service.RemoveAsync(id));
        }


    }
}
