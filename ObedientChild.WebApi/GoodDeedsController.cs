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
    public class GoodDeedsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGoodDeedService _service;

        public GoodDeedsController(IMapper mapper, IGoodDeedService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<GoodDeed>>> GetList()
        {
            var list = await _service.GetListAsync();

            return list;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<GoodDeed>> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);

            return item;
        }

        [HttpPut]
        public async Task Add(GoodDeed goodDeed)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(goodDeed);
            }
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
             await _service.DeleteAsync(id);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<GoodDeed>> Update(int id, [FromBody] GoodDeed goodDeed)
        {
            return await _service.UpdateAsync(goodDeed);
        }
    }
}
