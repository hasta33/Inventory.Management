using InventoryManagement.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {

        //Endpoint olmadığını beliritiyoruz
        [NonAction]
        public IActionResult CreateActionResult<T>(CustomResponseDto<T> response)
        {
            if (response.StatusCode == 204)
            {
                return new ObjectResult(null)
                {
                    StatusCode = response.StatusCode
                };
            }
            else if (response.StatusCode == 201)
            {

            }


            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };


        }
    }
}
