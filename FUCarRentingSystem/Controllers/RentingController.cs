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
                HttpContext.Response.Headers.Append("location", location);
                return Ok(new { location = location});
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

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetTransactionsOfCurrentUser(int userId)
        {
            HttpContext.HeaderAuthenticate(out int currentUserId);
            if(currentUserId == userId || currentUserId == -1)
            {
                return Ok(await rentingService.GetTransactionsOfUser(userId));
            }
            else
                throw new ForbidenException();
        }

        [HttpGet("statistic")]
        public async Task<IActionResult> GetStatistic([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            HttpContext.HeaderAuthenticate(out int userId);
            if(userId == -1)
            {
                List<StatictisResult> result = await rentingService.GetStatictisResult(start, end);
                return Ok(result);
            }
            throw new UnauthorizeExpcetion("Not allowed");
        }
    }
}
