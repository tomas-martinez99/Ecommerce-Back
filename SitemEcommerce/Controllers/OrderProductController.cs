using Application.GetAllDtos;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/orders/{orderId:int}/products")]
    public class OrderProductController : ControllerBase
    {
        private readonly IOrderProductService _service;

        public OrderProductController(IOrderProductService service)
        {
            _service = service;
        }

        // GET api/orders/{orderId}/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderProductDto>>> GetByOrderId(int orderId)
        {
            var list = await _service.GetByOrderIdAsync(orderId);
            return Ok(list);
        }
    }
}
