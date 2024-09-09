using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ObedientChild.App;
using ObedientChild.Domain;
using ObedientChild.Domain.LifeEnergy;
using ObedientChild.WebApi.LifeEnergy;
using Slid.Auth.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ObedientChild.WebApi
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class LifeEnergyController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILifeEnergyService _service;

        public LifeEnergyController(IMapper mapper, ILifeEnergyService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<LifeEnergyAccount>> GetAsync()
        {
            var userId = User.GetUserId();

            return await _service.GetAccountWithAccessCheckAsync(userId);
        }


        [HttpPost("powerup")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<LifeEnergyHistory>> PowerUpAsync([FromBody] LifeEnergyHistoryBindingModel history)
        {
            var userId = User.GetUserId();

            return await _service.PowerUpAsync(userId, history.Amount, history.Title);
        }


        [HttpPost("powerdown")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<LifeEnergyHistory>> PowerDownAsync([FromBody] LifeEnergyHistoryBindingModel history)
        {
            var userId = User.GetUserId();

            return await _service.PowerDownAsync(userId, history.Amount, history.Title);
        }

        [HttpGet("history")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<LifeEnergyHistory>>> GetHistoryList()
        {
            var userId = User.GetUserId();

            var list = await _service.GetHistoryListWithAccessCheckAsync(userId);

            return list;
        }

        [HttpGet("history/{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<LifeEnergyHistory>> GetHistoryById(int id)
        {
            var userId = User.GetUserId();

            var item = await _service.GetHistoryByIdAsync(userId, id);

            return item;
        }

        [HttpDelete("history/{id}")]
        public async Task RevertHistory(int id)
        {
            var userId = User.GetUserId();

            await _service.RevertHistoryAsync(userId, id);
        }

        [HttpPut]
        public async Task<LifeEnergyAccount> CreateAccount()
        {
            var userId = User.GetUserId();

            return await _service.CreateAccountAsync(userId);
        }

        [HttpDelete]
        public async Task RemoveAccount()
        {
            var userId = User.GetUserId();

            await _service.RemoveAccountAsync(userId);
        }
    }
}
