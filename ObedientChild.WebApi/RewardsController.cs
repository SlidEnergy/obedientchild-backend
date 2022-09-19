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
    public class RewardsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRewardsService _rewardsService;

        public RewardsController(IMapper mapper, IRewardsService rewardsService)
        {
            _mapper = mapper;
            _rewardsService = rewardsService;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Reward>>> GetList()
        {
            var list = await _rewardsService.GetListAsync();

            return list;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Reward>> GetById(int id)
        {
            var item = await _rewardsService.GetByIdAsync(id);

            return item;
        }

        [HttpPut]
        public async Task AddReward(Reward reward)
        {
            if (ModelState.IsValid)
            {
                await _rewardsService.AddRewardAsync(reward);
            }
        }

        [HttpDelete("{id}")]
        public async Task DeleteReward(int id)
        {
             await _rewardsService.DeleteRewardAsync(id);
        }
    }
}
