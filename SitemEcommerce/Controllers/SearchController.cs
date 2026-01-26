using Application.GetAllDtos;
using Application.Interfaces.Services;
using Domain.Errors;
using Microsoft.AspNetCore.Mvc;
using MovieInfo.Api.Infraestructure;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _service;
        public SearchController(ISearchService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<SearchProductResponseByName>>> SearchProduct([FromQuery] SearchProductRequest request)
        {
            var result = await _service.SearchProductResponseAsync(request);

            if (result.IsFailed)
            {
                var error = result.Errors.First();

                if (error is NotFoundError) return NotFound(new ApiErrorResponse("Not Found", error.Message));

                return BadRequest(new ApiErrorResponse("Errors", result.Errors.Select(e => e.Message)));
            }

            return Ok(result.Value);
        }
    }
}
