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
using ObedientChild.Domain.Tasks;
using Slid.Auth.Core;

namespace ObedientChild.WebApi
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class ChildTasksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IChildTasksService _service;

        public ChildTasksController(IMapper mapper, IChildTasksService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ChildTask>>> GetList()
        {
            var list = await _service.GetListAsync();

            return list;
        }

        [HttpGet("day")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ChildTask>>> GetListForDay([FromQuery] int childId, [FromQuery] DateOnly day)
        {
            // получаем объединенный список привычек активных на текущий момент + выполненные или пропущенные
            var list = await _service.GetListForDayAsync(childId, day);

            return list;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<ChildTask>> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);

            return item;
        }

        [HttpPut]
        public async Task Add(ChildTask model)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(model);
            }
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            // set end date or remove if doesn't exist in habit history
             await _service.DeleteAsync(id);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<ChildTask>> Update(int id, [FromBody] ChildTask model)
        {
            return await _service.UpdateAsync(model);
        }

        [HttpPost("{id}/status")]
        public async Task<ActionResult<ChildTask>> SetStatus(int id, [FromQuery] int childId, [FromQuery] ChildTaskStatus status)
        {
            var userId = User.GetUserId();

            return await _service.SetStatusAsync(userId, id, childId, status);
        }
    }
}
