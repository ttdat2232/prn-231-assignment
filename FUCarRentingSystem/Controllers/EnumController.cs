using Application.Dtos.Creates;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FUCarRentingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnumController : ControllerBase
    {
        [HttpGet("Cars/FuelTypes")]
        public IActionResult GetFuelTypes() 
        { 
            return Ok(Enum.GetValues(typeof(FuelType)));
        }

        [HttpGet("Cars/DoorNumber")]
        public IActionResult GetDoorNumberTypes()
        {
            return Ok(Enum.GetValues(typeof(DoorNumber)));
        }
    }
}
