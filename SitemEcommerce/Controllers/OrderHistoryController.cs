using Application.GetAllDtos;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/orders/{orderId:int}/history")]
    public class OrderHistoryController : ControllerBase
    {
        private readonly IOrderHistoryService _service;

        public OrderHistoryController(IOrderHistoryService service)
        {
            _service = service;
        }

        // GET api/orders/{orderId}/history
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderHistoryDto>>> GetByOrderId(int orderId)
        {
            var list = await _service.GetByOrderIdAsync(orderId);
            return Ok(list);
        }
    }
}
