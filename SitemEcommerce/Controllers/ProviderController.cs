using Application.CreateDtos;
using Application.GetAllDtos;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ProviderController : ControllerBase
    {
        private readonly IProviderService _service;
        public ProviderController(IProviderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProviderDto>>> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProviderDto>> GetById(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto is null) return NotFound();
            return Ok(dto);
        }

        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetProductsByProvider(int id)
        {
            var dto = await _service.GetByIdWithProductsAsync(id);
            if (dto == null) return NotFound();
            return Ok(dto);
        }

        [HttpGet("with-products")]
        public async Task<IActionResult> GetAllWithProducts()
        {
            var dtos = await _service.GetAllWithProductsAsync();
            return Ok(dtos);
        }

        [HttpPost]
        public async Task<ActionResult<ProviderDto>> Create(CreateProviderDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, CreateProviderDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
