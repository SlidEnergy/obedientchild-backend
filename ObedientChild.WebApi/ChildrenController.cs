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

        [HttpPost("{id}/earn/{count?}")]
        public async Task<int> EarnCoin(int id, int count = 1)
        {
            var balance = await _childrenService.EarnCountAsync(id, count);

            return balance;
        }

        [HttpPost("{id}/spend/{count?}")]
        public async Task<int> SpendCoin(int id, int count = 1)
        {
            var balance = await _childrenService.SpendCountAsync(id, count);

            return balance;
        }
    }
}
