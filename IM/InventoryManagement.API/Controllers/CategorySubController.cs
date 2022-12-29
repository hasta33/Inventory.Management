using InventoryManagement.Core.DTOs.Category;
using InventoryManagement.Core.DTOs.CategorySub;
using InventoryManagement.Core.Models;
using InventoryManagement.Core.Services;
using InventoryManagement.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorySubController : CustomBaseController
    {

        private readonly IServiceWithDto<CategorySub, CategorySubDto> _categorySubService;

        public CategorySubController(IServiceWithDto<CategorySub, CategorySubDto> categorySubService)
        {
            _categorySubService = categorySubService;
        }



        //[HttpGet]
        //public async Task<IActionResult> GetCategoriesSub()
        //{
        //    return CreateActionResult(await _categorySubServiceWithDto.GetAllAsync());
        //}

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetCategoriesSubById (int id)
        //{
        //    return CreateActionResult(await _categorySubServiceWithDto.GetByIdAsync(id));
        //}


        //[HttpPost]
        //public async Task<IActionResult> AddAsync(CategorySubDto createDto)   // CategorySubDto create dto almıyor kontrol edilecek
        //{
        //    return CreateActionResult(await _categorySubService.AddAsync(createDto));
        //}


        //[HttpPost]
        //public async Task<IActionResult> Get(CategorySubDto category)
        //{
        //    return CreateActionResult(await _categorySubService.AddAsync(category));
        //}


        //[HttpPut]
        //public async Task<IActionResult> UpdateAsync(CategorySubUpdateDto categorySubUpdate) // CategorySubDto updatedto almıyor kontrol edilecek
        //{
        //    //var result = _mapper.Map<Category>(categorySubUpdate);
        //    return CreateActionResult(await _categorySubServiceWithDto.UpdateAsync(categorySubUpdate));
        //}


        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Remove(int id)
        //{
        //    return CreateActionResult(await _categorySubServiceWithDto.RemoveAsync(id));
        //}




    }
}
