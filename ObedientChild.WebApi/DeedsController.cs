using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ObedientChild.App;
using ObedientChild.App.Habits;
using ObedientChild.Domain;
using ObedientChild.Domain.Habits;
using Slid.Auth.Core;

namespace ObedientChild.WebApi
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class DeedsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDeedsService _service;

        public DeedsController(IMapper mapper, IDeedsService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Deed>>> GetList([FromQuery]DeedType type)
        {
            var list = await _service.GetListAsync(type);

            return list;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Deed>> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);

            return item;
        }

        [HttpPut]
        public async Task Add(Deed habit)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(habit);
            }
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            // set end date or remove if doesn't exist in habit history
             await _service.DeleteAsync(id);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<Deed>> Update(int id, [FromBody] DeedDto model)
        {
            if (id != model.Id)
            {
                return BadRequest("ID doesn't match");
            }

            // Маппинг DTO на бизнес-объект
            var deed = _mapper.Map<Deed>(model);

            return await _service.UpdateAsync(deed, model.CharacterTraitIds);
        }

        [HttpPut("{id}/invoke")]
        public async Task<int> Invoke(int id, [FromQuery]int childId, [FromBody] Deed deed)
        {
            var userId = User.GetUserId();

            var balance = await _service.InvokeDeedAsync(childId, id, deed, userId);

            return balance;
        }
    }
}
