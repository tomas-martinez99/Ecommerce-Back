using Application.CreateDtos;
using Application.GetAllDtos;
using Application.Interfaces.Services;
using Domain.Errors;
using Microsoft.AspNetCore.Mvc;
using MovieInfo.Api.Infraestructure;
using FluentResults;

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ProviderDto>>> GetAll()
        {
            var result = await _service.GetAllAsync();

            if (result.IsFailed)
            {
                var messages = result.Errors.Select(e => e.Message);

                return BadRequest(new ApiErrorResponse("Errors", messages));
            }

            return Ok(result.Value);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProviderDto>> GetById(int id)
        {
            var dto = await _service.GetByIdAsync(id);

            if (dto.IsFailed)
            {
                var error = dto.Errors.First();

                if (error is NotFoundError)
                {
                    var messages = dto.Errors.Select(e => e.Message);

                    return NotFound(new ApiErrorResponse("NotFound", messages));
                }

                var errorMessages = dto.Errors.Select(e => e.Message);

                return BadRequest(new ApiErrorResponse("Errors", errorMessages));
            }
            return Ok(dto.Value);
        }

        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetProductsByProvider(int id)
        {
            var dto = await _service.GetByIdWithProductsAsync(id);

            if (dto.IsFailed)
            {
                var error = dto.Errors.First();

                if (error is NotFoundError) 
                {
                    var messages = dto.Errors.Select(e => e.Message);

                    return NotFound(new ApiErrorResponse("NotFound", messages));
                }

                var errorMessages = dto.Errors.Select(e => e.Message);

                return BadRequest(new ApiErrorResponse("Errors", errorMessages));
            }

            return Ok(dto.Value);
        }

        [HttpGet("with-products")]
        public async Task<IActionResult> GetAllWithProducts()
        {
            var dtos = await _service.GetAllWithProductsAsync();
            return Ok(dtos);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProviderDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ProviderDto>> Create(CreateProviderDto dto)
        {
            var created = await _service.CreateAsync(dto);

            if (created.IsFailed)
            {
                var error = created.Errors.First();

                var errorMessages = created.Errors.Select(e => e.Message);

                return BadRequest(new ApiErrorResponse("Errors", errorMessages));
            }
            return CreatedAtAction(nameof(GetById), new { id = created.Value.Id }, created.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Update(int id, CreateProviderDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);

            if (updated.IsFailed)
            {
                var error = updated.Errors.First();

                if (error is NotFoundError)
                {
                    var messages = updated.Errors.Select(e => e.Message);

                    return NotFound(new ApiErrorResponse("NotFound", messages));
                }

                var errorMessages = updated.Errors.Select(e => e.Message);

                return BadRequest(new ApiErrorResponse("Errors", errorMessages));
            }
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);

            if (deleted.IsFailed)
            {
                var error = deleted.Errors.First();

                if (error is NotFoundError)
                {
                    var messages = deleted.Errors.Select(e => e.Message);

                    return NotFound(new ApiErrorResponse("NotFound", messages));
                }

                var errorMessages = deleted.Errors.Select(e => e.Message);
                
                return BadRequest(new ApiErrorResponse("Errors", errorMessages));
            }
            return NoContent();
        }
    }
}
