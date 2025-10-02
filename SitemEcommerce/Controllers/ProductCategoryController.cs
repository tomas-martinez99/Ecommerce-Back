using Application.CreateDtos;
using Application.DetailDtos;
using Application.GetAllDtos;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/productcategories")]
    public class ProductCategoriesController : ControllerBase
    {
        private readonly IProductCategoryService _service;

        public ProductCategoriesController(IProductCategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{productId:int}/{categoryId:int}")]
        public async Task<ActionResult<DetailProductCategoryDto>> GetByIds(int productId, int categoryId)
        {
            var dto = await _service.GetByIdsAsync(productId, categoryId);
            if (dto is null) return NotFound();
            return Ok(dto);
        }

        [HttpGet("product/{productId:int}")]
        public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> GetByProduct(int productId)
        {
            var list = await _service.GetByProductIdAsync(productId);
            return Ok(list);
        }

        [HttpGet("category/{categoryId:int}")]
        public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> GetByCategory(int categoryId)
        {
            var list = await _service.GetByCategoryIdAsync(categoryId);
            return Ok(list);
        }

        [HttpPost]
        public async Task<ActionResult<ProductCategoryDto>> Create([FromBody] CreateProductCategoryDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(
                nameof(GetByIds),
                new
                {
                    productId = created.ProductId,
                    categoryId = created.CategoryId
                },
                created
            );
        }

        [HttpDelete("{productId:int}/{categoryId:int}")]
        public async Task<IActionResult> Delete(int productId, int categoryId)
        {
            var deleted = await _service.DeleteAsync(productId, categoryId);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}