using InventoryManagement.Core.DTOs.Brand;
using InventoryManagement.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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
        [Authorize(Roles = "BrandRole", Policy = "brand#get")]
        public async Task<IActionResult> GetBrandList(int companyId, int page, int pageSize)
        {
            return CreateActionResult(await _service.GetBrandList(companyId, page, pageSize));
        }


        [HttpGet("{companyId}")]
        [Authorize(Roles = "BrandRole", Policy = "brand#get")]
        public async Task<IActionResult> GetBrandList(int companyId)
        {
            return CreateActionResult(await _service.GetBrandList(companyId));
        }


        [HttpGet]
        [Authorize(Roles = "BrandRole", Policy = "brand#get")]
        public async Task<IActionResult> GetBrandAllList()
        {
            return CreateActionResult(await _service.GetBrandAllList());
        }


        [HttpPost]
        [Authorize(Roles = "BrandRole", Policy = "brand#create")]
        public async Task<IActionResult> AddAsync([FromBody] BrandCreateDto dto)
        {
            return CreateActionResult(await _service.AddAsync(dto));
        }


        [HttpPut]
        [Authorize(Roles = "BrandRole", Policy = "brand#update")]
        public async Task<IActionResult> UpdateAsync([FromBody] BrandUpdateDto dto)
        {
            return CreateActionResult(await _service.UpdateAsync(dto));
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "BrandRole", Policy = "brand#delete")]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            return CreateActionResult(await _service.RemoveAsync(id));
        }


    }
}
