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
    public class DestiniesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPersonalitiesService _service;

        public DestiniesController(IMapper mapper, IPersonalitiesService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Destiny>>> GetList()
        {
            var list = await _service.GetDestinyListAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Destiny>> GetById(int id)
        {
            var item = await _service.GetDestinyByIdAsync(id);
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Destiny>> Add([FromBody] Destiny destiny)
        {
            if (ModelState.IsValid)
            {
                return await _service.AddDestinyAsync(destiny);
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Destiny>> Update(int id, [FromBody] Destiny destiny)
        {
            if (ModelState.IsValid && id == destiny.Id)
            {
                return await _service.UpdateDestinyAsync(destiny);
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.DeleteDestinyAsync(id);
            return NoContent();
        }
    }
}
