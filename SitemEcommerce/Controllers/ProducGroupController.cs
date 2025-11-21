using Application.CreateDtos;
using Application.GetAllDtos;
using Application.Interfaces.Services;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/ProductGroup")]
    public class ProductGroupController : ControllerBase
    {
        private readonly IProductGroupService _service;

        public ProductGroupController(IProductGroupService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var brand = await _service.GetByIdAsync(id);
            return brand is null ? NotFound() : Ok(brand);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductGroupDto dto)
        {
            var productGroup =await _service.AddAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = productGroup.Id }, productGroup);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateProductGroupDto dto)
        {
   
            await _service.UpdateAsync(dto, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }

}
