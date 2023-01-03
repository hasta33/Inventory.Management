using InventoryManagement.Core.DTOs.CategorySub;
using InventoryManagement.Core.Models;
using InventoryManagement.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorySubController : CustomBaseController
    {

        private readonly IServiceWithDto<CategorySub, CategorySubDto> _service;

        public CategorySubController(IServiceWithDto<CategorySub, CategorySubDto> service)
        {
            _service = service;
        }




        [HttpPost]
        public async Task<IActionResult> AddAsync(CategorySubDto dto)
        {
            return CreateActionResult(await _service.AddAsync(dto));
        }
    }
}
