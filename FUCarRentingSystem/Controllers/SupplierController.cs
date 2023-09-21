using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FUCarRentingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            this.supplierService = supplierService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSupplierAsync() 
        {
            return Ok(await supplierService.GetSuppliersAsync());
        }
    }
}
