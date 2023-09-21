using Application.Dtos;
using Application.Dtos.Creates;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FUCarRentingSystem.Utilities;
using Application.Exceptions;
using System.Net;

namespace FUCarRentingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentingController : ControllerBase
    {
        private readonly IRentingService rentingService;

        public RentingController(IRentingService rentingService)
        {
            this.rentingService = rentingService;
        }

        [HttpPost]
        public async Task<IActionResult> RentingCarAsync(List<CreateRentingDetailDto> dtos)
        {
            HttpContext.HeaderAuthenticate(out var userId);
            if (userId > 0)
            {
                RentingTransactionDto result = await rentingService.CreateRentingTransactionAsync(userId, dtos);
                var location = Request.Path.Value + $"?userId={userId}&transactionId={result.RentingTransationId}";
                Response.Headers.Append("location", location);
                return StatusCode((int)HttpStatusCode.Created);
            }
            else
                throw new ForbidenException();
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionByUserIdAndTractionId([FromQuery] int userId, [FromQuery] int transactionId)
        {
            HttpContext.HeaderAuthenticate(out int currentUserId);
            if (currentUserId == userId || currentUserId == -1)
            {
                RentingTransactionDto result = await rentingService.GetTransactionByUserIdAndTractionId(userId, transactionId);
                return Ok(result);
            }
                throw new ForbidenException();
        }
    }
}
