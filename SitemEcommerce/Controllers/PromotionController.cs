using Application.CreateDtos;
using Application.DetailDtos;
using Application.GetAllDtos;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionService _service;
        private readonly IMapper _mapper;

        public PromotionController(IPromotionService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // 🔹 GET: api/promotions/active
        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<PromotionDto>>> GetActivePromotions()
        {
            var promos = await _service.GetActivePromotionsAsync();
            return Ok(_mapper.Map<IEnumerable<PromotionDto>>(promos));
        }

        [HttpGet("all")]

        public async Task<ActionResult<IEnumerable<PromotionDto>>> GetAllPromotions()
        {
            var promos = await _service.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<PromotionDto>>(promos));
        }
        // 🔹 GET: api/promotions/5

        [HttpGet("{id:int}")]
        public async Task<ActionResult<DetailPromotionDto>> GetById(int id)
        {
            var promo = await _service.GetByIdAsync(id);
            if (promo is null) return NotFound();
            return Ok(_mapper.Map<DetailPromotionDto>(promo));
        }

        [HttpPost]
        public async Task<ActionResult<DetailPromotionDto>> Create([FromBody] CreatePromotionDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }


        // 🔹 PUT: api/promotions/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreatePromotionDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            return updated ? NoContent() : NotFound();
        }
        // 🔹 DELETE: api/promotions/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }

        [HttpPut("{id:int}/enable")]
        public async Task<IActionResult> Enable(int id)
        {
            var success = await _service.SetEnabledAsync(id, true);
            return success ? NoContent() : NotFound();
        }

        [HttpPut("{id:int}/disable")]
        public async Task<IActionResult> Disable(int id)
        {
            var success = await _service.SetEnabledAsync(id, false);
            return success ? NoContent() : NotFound();
        }
    }
}
