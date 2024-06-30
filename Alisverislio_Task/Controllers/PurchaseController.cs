using Alisverislio_Task.BLL.AbstractServices;
using Alisverislio_Task.BLL.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Alisverislio_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpPost("purchase")]
        public async Task<IActionResult> PurchaseBook([FromBody] PurchaseDto purchaseDto)
        {
            var purchase = await _purchaseService.PurchaseBookAsync(purchaseDto.UserId, purchaseDto.BookId);
            if (purchase == null)
                return BadRequest("Purchase could not be completed.");

            return Ok(purchase);
        }
    }
}
