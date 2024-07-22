using System.Collections;
using AutoMapper;
using ObedientChild.App;
using ObedientChild.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Slid.Auth.Core;

namespace ObedientChild.WebApi
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ChildrenController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IChildrenService _childrenService;

        public ChildrenController(IMapper mapper, IChildrenService childrenService)
        {
            _mapper = mapper;
            _childrenService = childrenService;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Dto.Child>>> GetList()
        {
            var list = await _childrenService.GetListAsync();

            return _mapper.Map<Dto.Child[]>(list);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Dto.Child>> GetById(int id)
        {
            var item = await _childrenService.GetByIdAsync(id);

            return _mapper.Map<Dto.Child>(item); ;
        }

        [HttpPost("{id}/avatar")]
        public async Task SaveAvatar(int id, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.Length > 0)
                {

                    byte[] data = null;
                    using (var fs1 = image.OpenReadStream())
                    using (var ms1 = new MemoryStream())
                    {
                        fs1.CopyTo(ms1);
                        data = ms1.ToArray();
                    }

                    await _childrenService.SaveAvatarAsync(id, data);
                }
            }
        }

        [HttpGet("{id}/avatar.png")]
        public async Task<IActionResult> GetAvatar(int id)
        {
            var child = await _childrenService.GetByIdAsync(id);

            return File(child.Avatar, "image/png");
        }

        [HttpPut("{id}/earn/{count}")]
        public async Task<int> EarnCoinPut(int id, int count)
        {
            var balance = await _childrenService.EarnCountAsync(id, count);

            return balance;
        }

        [HttpPost("{id}/earn/{count}")]
        public async Task<int> EarnCoinPost(int id, int count)
        {
            var balance = await _childrenService.EarnCountAsync(id, count);

            return balance;
        }

        [HttpPut("{id}/earn")]
        public async Task<int> EarnCoin(int id, [FromBody]Reward reward)
        {
            var balance = await _childrenService.EarnAsync(id, reward);

            return balance;
        }

        [HttpPut("{id}/spend/{count}")]
        public async Task<int> SpendCoinPut(int id, int count)
        {
            var balance = await _childrenService.SpendCountAsync(id, count);

            return balance;
        }

        [HttpPost("{id}/spend/{count}")]
        public async Task<int> SpendCoinPost(int id, int count)
        {
            var balance = await _childrenService.SpendCountAsync(id, count);

            return balance;
        }

        [HttpPut("{id}/spend")]
        public async Task<int> SpendCoin(int id, [FromBody]Reward reward)
        {
            var balance = await _childrenService.SpendAsync(id, reward);

            return balance;
        }

        [HttpPost("{id}/setgoal")]
        public async Task SetGoal(int id, [FromBody] int rewardId)
        {
            await _childrenService.SetGoalAsync(id, rewardId);
        }

        [HttpPost("{id}/setdream")]
        public async Task SetDream(int id, [FromBody] int rewardId)
        {
            await _childrenService.SetDreamAsync(id, rewardId);
        }
    }
}
