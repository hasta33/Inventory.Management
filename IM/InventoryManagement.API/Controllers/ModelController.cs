using InventoryManagement.Core.DTOs.Model;
using InventoryManagement.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelController : CustomBaseController
    {
        private readonly IModelServiceWithDto _service;

        public ModelController(IModelServiceWithDto service)
        {
            _service = service;
        }


        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] ModelCreateDto dto)
        {
            return CreateActionResult(await _service.AddAsync(dto));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] ModelUpdateDto dto)
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
