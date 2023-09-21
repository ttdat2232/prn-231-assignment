using Application.Dtos.Creates;
using Application.Dtos.Updates;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using FUCarRentingSystem.Utilities;
using Application.Exceptions;

namespace FUCarRentingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarInformationController : ControllerBase
    {
        private readonly ICarService carService;

        public CarInformationController(ICarService carService)
        {
            this.carService = carService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCarInformationsAsync(int pageIndex = 0, int pageSize = 1, string? name = "", [FromQuery] int[]? statuses = null) 
        {
            return Ok(await carService.GetCarInformationAsync(pageIndex, pageSize, name, statuses));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarInformationByIdAsync(int id)
        {
            return Ok(await carService.GetCarInformationByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddCarInformationAsync(CreateCarInformationDto dto)
        {
            HttpContext.HeaderAuthenticate(out int userId);
            if (userId != -1)
                throw new ForbidenException();
            var result = await carService.AddCarInformationAsync(dto);
            var location = $"{HttpContext.Request.Path.Value}/{result.CarId}";
            HttpContext.Response.Headers.Add("location", location);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCarInformationAsync(int id, UpdateCarInformationDto dto)
        {
            HttpContext.HeaderAuthenticate(out int userId);
            if (userId != -1)
                throw new ForbidenException();
            dto.CarId = id;
            await carService.UpdateCarInformation(dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarInformationAsync(int id)
        {
            HttpContext.HeaderAuthenticate(out int userId);
            if (userId != -1)
                throw new ForbidenException();
            await carService.DeleteCarInformation(id); 
            return Ok();
        }
    }
}
