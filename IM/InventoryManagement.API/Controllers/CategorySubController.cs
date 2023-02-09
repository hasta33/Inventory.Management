using InventoryManagement.Core.DTOs.CategorySub;
using InventoryManagement.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorySubController : CustomBaseController
    {
        private readonly ICategorySubServiceWithDto _service;

        public CategorySubController(ICategorySubServiceWithDto service)
        {
            _service = service;
        }


        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CategorySubCreateDto dto)
        {
            return CreateActionResult(await _service.AddAsync(dto));
        }


        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] CategorySubUpdateDto dto)
        {
            return CreateActionResult(await _service.UpdateAsync(dto));
        }
    }
}
