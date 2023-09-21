using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FUCarRentingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturerController : ControllerBase
    {
        private readonly IManufacturerService manufacturerService;

        public ManufacturerController(IManufacturerService manufacturerService)
        {
            this.manufacturerService = manufacturerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetManuafacturerAsync()
        {
            return Ok(await manufacturerService.GetManufacturerAsync());
        }
    }
}
