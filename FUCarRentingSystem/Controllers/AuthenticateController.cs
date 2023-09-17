using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FUCarRentingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticationSerivce authenticationSerivce;

        public AuthenticateController(IAuthenticationSerivce authenticationSerivce)
        {
            this.authenticationSerivce = authenticationSerivce;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(AuthenticateRequest request)
        {
            return Ok(await authenticationSerivce.LoginAsync(request));
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(AuthenticateRequest request)
        {
            return Ok(await authenticationSerivce.RegisterAsync(request));
        }
    }
}
