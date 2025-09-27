using Application.CreateDtos;
using Application.GetAllDtos;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/categories/{categoryId:int}/values")]
    public class ValueCategoryController : ControllerBase
    {
        private readonly IValueCategoryService _service;
        public ValueCategoryController(IValueCategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<ValueCategoryDto>>> GetByCategory(int categoryId)
        {
            var list = await _service.GetByCategoryIdAsync(categoryId);
            return Ok(list);
        }

        [HttpPost]
        public async Task<ActionResult<ValueCategoryDto>> Create(
            int categoryId,
            [FromBody] CreateValueCategoryDto dto)
        {
            var created = await _service.CreateAsync(dto, categoryId);
            // Retornamos 201 con ubicación relativa al collection GET
            return CreatedAtAction(
                nameof(GetByCategory),
                new { categoryId },
                created
            );
        }

        [HttpDelete("{valueId:int}")]
        public async Task<IActionResult> Delete(int categoryId, int valueId)
        {
            var deleted = await _service.DeleteAsync(valueId);
            if (!deleted)
                return NotFound();
            return NoContent();
        }


    }
}
