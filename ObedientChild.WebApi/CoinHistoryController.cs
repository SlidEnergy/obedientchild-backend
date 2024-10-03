using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ObedientChild.App;
using ObedientChild.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ObedientChild.WebApi
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CoinHistoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBalanceHistoryService _service;

        public CoinHistoryController(IMapper mapper, IBalanceHistoryService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<BalanceHistory>>> GetList([FromQuery]BalanceType type, [FromQuery] int childId = 0)
        {
            var list = await _service.GetListAsync(childId, type);

            return list;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<BalanceHistory>> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);

            return item;
        }

        [HttpDelete("{id}")]
        public async Task Revert(int id)
        {
            await _service.RevertAsync(id);
        }
    }
}
