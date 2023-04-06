using InventoryManagement.Core.DTOs;
using InventoryManagement.Core.DTOs.Inventory;
using InventoryManagement.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : CustomBaseController
    {
        private readonly IInventoryServiceWithDto _service;

        public InventoryController(IInventoryServiceWithDto service)
        {
            _service = service;
        }



        [HttpGet("{page}/{pageSize}")]
        [Authorize(Roles = "InventoryRole", Policy = "inventory#get")]
        public async Task<IActionResult> GetInventoryList([FromQuery]FilteringParameters parameters, int page, int pageSize)
        {
            return CreateActionResult(await _service.GetInventoryList(parameters, page, pageSize));
        }



        [HttpPost]
        [Authorize(Roles = "InventoryRole", Policy = "inventory#create")]
        public async Task<IActionResult> AddAsync([FromBody] InventoryCreateDto dto)
        {
            return CreateActionResult(await _service.AddAsync(dto));
        }

        [HttpPut]
        [Authorize(Roles = "InventoryRole", Policy = "inventory#update")]
        public async Task<IActionResult> UpdateAsync([FromBody] InventoryUpdateDto dto)
        {
            return CreateActionResult(await _service.UpdateAsync(dto));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "InventoryRole", Policy = "inventory#delete")]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            return CreateActionResult(await _service.RemoveAsync(id));
        }


    }
}
