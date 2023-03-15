using InventoryManagement.Core.DTOs.Brand;
using InventoryManagement.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : CustomBaseController
    {
        private readonly IBrandServiceWithDto _service;

        public BrandController(IBrandServiceWithDto service)
        {
            _service = service;
        }



        [HttpGet("{companyId}/{page}/{pageSize}")]
        public async Task<IActionResult> GetBrandList(int companyId, int page, int pageSize)
        {
            return CreateActionResult(await _service.GetBrandList(companyId, page, pageSize));
        }


        [HttpGet("{companyId}")]
        public async Task<IActionResult> GetBrandList(int companyId)
        {
            return CreateActionResult(await _service.GetBrandList(companyId));
        }


        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] BrandCreateDto dto)
        {
            return CreateActionResult(await _service.AddAsync(dto));
        }


        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] BrandUpdateDto dto)
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
