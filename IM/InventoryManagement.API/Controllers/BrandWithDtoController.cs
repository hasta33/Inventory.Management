using InventoryManagement.Core.DTOs.BrandModel.Brand;
using InventoryManagement.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandWithDtoController : CustomBaseController
    {
        private readonly IBrandServiceWithDto _brandServiceWithDto;

        public BrandWithDtoController(IBrandServiceWithDto brandServiceWithDto)
        {
            _brandServiceWithDto = brandServiceWithDto;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllBrands()
        {
            return CreateActionResult(await _brandServiceWithDto.GetAllAsync());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrandByIdWithModel(int id)
        {
            return CreateActionResult(await _brandServiceWithDto.GetBrandByIdWithModel(id));
        }


        [HttpPost]
        public async Task<IActionResult> AddAsync(BrandCreateDto dto)
        {
            return CreateActionResult(await _brandServiceWithDto.AddAsync(dto));
        }


        [HttpPut]
        public async Task<IActionResult> UpdateAsync(BrandUpdateDto dto)
        {
            return CreateActionResult(await _brandServiceWithDto.UpdateAsync(dto));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            return CreateActionResult(await _brandServiceWithDto.RemoveAsync(id));
        }


        [HttpPost("SaveAll")]
        public async Task<IActionResult> SaveAll(List<BrandDto> dtos)
        {
            return CreateActionResult(await _brandServiceWithDto.AddRangeAsync(dtos));
        }



        [HttpDelete("RemoveAll")]
        public async Task<IActionResult> RemoveAll(List<int> ids)
        {
            return CreateActionResult(await _brandServiceWithDto.RemoveRangeAsync(ids));
        }



    }
}
