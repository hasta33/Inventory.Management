using InventoryManagement.Core.DTOs.Category;
using InventoryManagement.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : CustomBaseController
    {
        private readonly ICategoryServiceWithDto _categoryServiceWithDto;

        public CategoryController(ICategoryServiceWithDto categoryServiceWithDto)
        {
            _categoryServiceWithDto = categoryServiceWithDto;
        }


        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            return CreateActionResult(await _categoryServiceWithDto.GetCategories());
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoriesById(int id)
        {
            return CreateActionResult(await _categoryServiceWithDto.GetByIdAsync(id));
        }


        [HttpGet("{page}/{pageSize}")]
        public async Task<IActionResult> GetCategoriesPageList(int page, int pageSize)
        {
            return CreateActionResult(await _categoryServiceWithDto.GetCategoriesPageList(page, pageSize));
        }



        [HttpPost]
        public async Task<IActionResult> AddAsync(CategoryCreateDto createDto)
        {
            return CreateActionResult(await _categoryServiceWithDto.AddAsync(createDto));
        }

        [HttpPost("AddRangeAsync")]
        public async Task<IActionResult> AddRangeAsync(List<CategoryDto> categories)
        {
            return CreateActionResult(await _categoryServiceWithDto.AddRangeAsync(categories));
        }


        [HttpPut]
        public async Task<IActionResult> UpdateAsync(CategoryUpdateDto categoryUpdate)
        {
            return CreateActionResult(await _categoryServiceWithDto.UpdateAsync(categoryUpdate));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            return CreateActionResult(await _categoryServiceWithDto.RemoveAsync(id));
        }

        [HttpDelete("DeleteAllRange")]
        public async Task<IActionResult> RemoveRangeAsync(List<int> ids)
        {
            return CreateActionResult(await _categoryServiceWithDto.RemoveRangeAsync(ids));
        }

        //Silme işlemi yapılmıyor, sadece true false değerlerini döndürüyor
        [HttpDelete("Any/{id}")]
        public async Task<IActionResult> AnyAsync(int id)
        {
            return CreateActionResult(await _categoryServiceWithDto.AnyAsync(x => x.Id == id));
        }

    }
}
