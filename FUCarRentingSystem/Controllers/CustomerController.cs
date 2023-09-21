using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FUCarRentingSystem.Utilities;
using Application.Exceptions;
using Application.Dtos.Updates;

namespace FUCarRentingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerByIdAsync(int id)
        {
            HttpContext.HeaderAuthenticate(out int userId);
            if (userId == -1 || userId == id)
            {
                return Ok(await customerService.GetCustomerByIdAsync(id));
            }
            else
                throw new ForbidenException();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerProfileAsync(int id, UpdateCustomerInformationDto dto)
        {
            HttpContext.HeaderAuthenticate(out int userId);
            if (userId == -1 || userId == id)
            {
                return Ok(await customerService.UpdateCustomerProfileAsync(id, dto));
            }
            else
                throw new ForbidenException();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerAsync(int id)
        {
            HttpContext.HeaderAuthenticate(out int userId);
            if (userId == -1 || userId == id)
            {
                await customerService.DeleteCustomerAsync(id)
                    .ContinueWith(t =>
                    {
                        if (t.IsCompletedSuccessfully && userId == id)
                            HttpContext.Response.Cookies.Delete("Authentication");
                    });
                return Ok();
            }
            else
                throw new ForbidenException();
        }
    }
}
