using AutoMapper;
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
        private readonly IMapper _mapper;
        public CategorySubController(IServiceWithDto<CategorySub, CategorySubDto> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }



        [HttpGet]
        public async Task<IActionResult> GetSubCategories()
        {
            return CreateActionResult(await _service.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(CategorySubCreateDto dto)
        {
            var result = _mapper.Map<CategorySubDto>(dto);
            return CreateActionResult(await _service.AddAsync(result));
        }


        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] CategorySubUpdateDto dto)
        {
            var result = _mapper.Map<CategorySubDto>(dto);
            return CreateActionResult(await _service.UpdateAsync(result));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            return CreateActionResult(await _service.RemoveAsync(id));
        }


    }
}
