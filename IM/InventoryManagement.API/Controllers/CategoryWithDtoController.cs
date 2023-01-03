using InventoryManagement.Core.DTOs.Category;
using InventoryManagement.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryWithDtoController : CustomBaseController
    {
        private readonly ICategoryServiceWithDto _service;

        public CategoryWithDtoController(ICategoryServiceWithDto service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            return CreateActionResult(await _service.GetAllAsync());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryByIdWithSubCategory(int id)
        {
            return CreateActionResult(await _service.GetCategoryByIdWithSubCategory(id));
        }



        [HttpPost]
        public async Task<IActionResult> AddAsync(CategoryCreateDto dto)
        {
            return CreateActionResult(await _service.AddAsync(dto));
        }



        [HttpPut]
        public async Task<IActionResult> UpdateAsync(CategoryUpdateDto dto)
        {
            return CreateActionResult(await _service.UpdateAsync(dto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            return CreateActionResult(await _service.RemoveAsync(id));
        }



        [HttpPost("SaveAll")]
        public async Task<IActionResult> SaveAll(List<CategoryDto> dtos)
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
