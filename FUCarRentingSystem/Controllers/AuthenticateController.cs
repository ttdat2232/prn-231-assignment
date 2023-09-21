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
            var result = await authenticationSerivce.LoginAsync(request);
            SetHeaders(result);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(AuthenticateRequest request)
        {
            var result = await authenticationSerivce.RegisterAsync(request);
            SetHeaders(result);
            return Ok(result);
        }

        private void SetHeaders(AuthenticateResponse response)
        {
            HttpContext.Response.Headers
                .Append("Authorization", response.CustomerId.ToString()
                //, new CookieOptions()
                //{
                //    HttpOnly = true,
                //    Secure = true
                //}
                );
        }
    }
}
