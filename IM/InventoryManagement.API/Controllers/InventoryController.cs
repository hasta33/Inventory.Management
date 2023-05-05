using InventoryManagement.API.Services;
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
        //private readonly IInventoryMovementServiceWithDto _movementService;
        private readonly ILogger<InventoryController> _logger;
        public InventoryController(IInventoryServiceWithDto service, ILogger<InventoryController> logger = null)
        {
            _service = service;
            _logger = logger;
            //_movementService = movementService;
        }



        [HttpGet("{page}/{pageSize}")]
        //[Authorize(Roles = "InventoryRole", Policy = "inventory#get")]
        public async Task<IActionResult> GetInventoryList([FromQuery]FilteringParameters parameters, int page, int pageSize)
        {
            return CreateActionResult(await _service.GetInventoryList(parameters, page, pageSize));
        }



        [HttpPost]
        [Authorize(Roles = "InventoryRole", Policy = "inventory#create")]
        public async Task<IActionResult> AddAsync([FromBody] InventoryCreateDto dto)
        {
            //var movement = new InventoryMovementCreateDto()
            //{
            //    Company = dto.CompanyId.ToString(),

            //};
            return CreateActionResult(await _service.AddAsync(dto));
        }



        [HttpPut]
        [Authorize(Roles = "InventoryRole", Policy = "inventory#update")]
        public async Task<IActionResult> UpdateAsync([FromBody] InventoryUpdateDto dto)
        {
            return CreateActionResult(await _service.UpdateAsync(dto));
        }



        [HttpPut("embezzled")]
        public async Task<IActionResult> InventoryEmbezzled([FromBody] InventoryEmbezzledDto dto)
        {
            ////_logger2.LogWarning("sdfsddsf");
            _logger.LogWarning("Hata geliyor...");
            //_logger.LogError("fdsfdsfdsf");
            //_logger.LogInformation("sdfdsf");
            //_logger.LogTrace("sdfsdf");
            //_logger.LogCritical("dsfdsfdsfsfd");

            throw new Exception("sonuncu olan throw");

            return Ok();
            //return CreateActionResult(await _service.InventoryEmbezzled(dto));
        }



        [HttpDelete("{id}")]
        [Authorize(Roles = "InventoryRole", Policy = "inventory#delete")]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            return CreateActionResult(await _service.RemoveAsync(id));
        }


    }
}
