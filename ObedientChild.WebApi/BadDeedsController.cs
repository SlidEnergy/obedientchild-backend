using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ObedientChild.App;
using ObedientChild.Domain;

namespace ObedientChild.WebApi
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BadDeedsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBadDeedService _service;

        public BadDeedsController(IMapper mapper, IBadDeedService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<BadDeed>>> GetList()
        {
            var list = await _service.GetListAsync();

            return list;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<BadDeed>> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);

            return item;
        }

        [HttpPut]
        public async Task Add(BadDeed badDeed)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(badDeed);
            }
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
             await _service.DeleteAsync(id);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<BadDeed>> Update(int id, [FromBody] BadDeed badDeed)
        {
            return await _service.UpdateAsync(badDeed);
        }
    }
}
