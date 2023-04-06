using InventoryManagement.Core.DTOs.InventoryMovement;
using InventoryManagement.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryMovementController : CustomBaseController
    {
        private readonly IInventoryMovementServiceWithDto _service;

        public InventoryMovementController(IInventoryMovementServiceWithDto service)
        {
            _service = service;
        }


        [HttpGet("{inventoryId}")]
        public async Task<IActionResult> GetInventoryMovement(int inventoryId)
        {
            return CreateActionResult(await _service.GetInventoryMovement(inventoryId));
        }



        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] InventoryMovementCreateDto dto)
        {
            return CreateActionResult(await _service.AddAsync(dto));
        }
    }
}
