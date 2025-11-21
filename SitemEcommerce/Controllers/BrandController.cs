using Application.CreateDtos;
using Application.GetAllDtos;
using Application.Interfaces.Services;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/Brand")]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _service;

        public BrandsController(IBrandService service)
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
        public async Task<IActionResult> Create(CreateBrandDto dto)
        {
            var brand = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = brand.Id }, brand);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateBrandDto dto)
        {
         
            await _service.UpdateAsync(dto,id);
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
