using Application.CreateDtos;
using Application.DetailDtos;
using Application.GetAllDtos;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }
        [HttpGet("admin")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProvider()
        {
            var list = await _service.GetAllProviderAsync();
            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<DetailProductDto>> GetById(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto is null) return NotFound();
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<DetailProductDto>> Create(CreateProductDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody]CreateProductDto dto)
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

        // 🔹 POST: api/products/5/images
        [HttpPost("{id}/images")]
        public async Task<ActionResult<ProductImageDto>> AddImage(int id, IFormFile file, [FromQuery] bool isMain = false)
        {
            if (file == null || file.Length == 0) return BadRequest("No se subió ninguna imagen.");

            var image = await _service.AddImageAsync(id, file, isMain);
            return Ok(image);
        }

        // 🔹 DELETE: api/products/5/images/10
        [HttpDelete("{id}/images/{imageId}")]
        public async Task<IActionResult> RemoveImage(int id, int imageId)
        {
            var success = await _service.RemoveImageAsync(id, imageId);
            if (!success) return NotFound();
            return NoContent();
        }

        // 🔹 PUT: api/products/5/images/10/set-main
        [HttpPut("{id}/images/{imageId}/set-main")]
        public async Task<IActionResult> SetMainImage(int id, int imageId)
        {
            var success = await _service.SetMainImageAsync(id, imageId);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
