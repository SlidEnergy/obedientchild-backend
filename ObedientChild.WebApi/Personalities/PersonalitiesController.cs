using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ObedientChild.App.Personalities;
using ObedientChild.Domain.Personalities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ObedientChild.WebApi.Personalities
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PersonalitiesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPersonalitiesService _service;

        public PersonalitiesController(IMapper mapper, IPersonalitiesService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Personality>>> GetList()
        {
            var list = await _service.GetListAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Personality>> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Personality>> Add([FromBody] CreatePersonalityInputModel model)
        {
            if (ModelState.IsValid)
            {
                return await _service.AddAsync(model.DestinyIds, model.Personality);
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Personality>> Update(int id, [FromBody] Personality personality)
        {
            if (ModelState.IsValid && id == personality.Id)
            {
                return await _service.UpdateAsync(personality);
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
