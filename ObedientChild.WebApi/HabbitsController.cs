using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ObedientChild.App;
using ObedientChild.App.Habbits;
using ObedientChild.Domain;
using ObedientChild.Domain.Habbits;

namespace ObedientChild.WebApi
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class HabbitsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHabbitsService _service;

        public HabbitsController(IMapper mapper, IHabbitsService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Habbit>>> GetList()
        {
            var list = await _service.GetListAsync();

            return list;
        }

        [HttpGet("day")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<DayHabbit>>> GetListForDay([FromQuery] int childId, [FromQuery] DateOnly day)
        {
            // получаем объединенный список привычек активных на текущий момент + выполненные или пропущенные
            var list = await _service.GetListForDayAsync(childId, day);

            return list;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Habbit>> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);

            return item;
        }

        [HttpPut]
        public async Task Add(Habbit habbit)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(habbit);
            }
        }

        [HttpPost("{habbitId}/child/{childId}")]
        public async Task SetForChild(int habbitId, int childId)
        {
            if (ModelState.IsValid)
            {
                await _service.SetForChildAsync(childId, habbitId);
            }
        }


        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            // set end date or remove if doesn't exist in habbit history
             await _service.DeleteAsync(id);
        }

        [HttpDelete("{habbitId}/child/{childId}")]
        public async Task UnsetForChild(int habbitId, int childId)
        {
            await _service.UnsetForChildAsync(habbitId, childId);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<Habbit>> Update(int id, [FromBody] Habbit model)
        {
            return await _service.UpdateAsync(model);
        }

        [HttpPost("{id}/status")]
        public async Task<ActionResult<HabbitHistory>> SetStatus(int id, [FromQuery] int childId, [FromQuery] DateOnly day, [FromQuery] HabbitHistoryStatus status)
        {
            return await _service.SetStatusAsync(id, childId, day, status);
        }

        [HttpGet("statistics")]
        public async Task<ActionResult<WeekHabbitStatistic>> GetStatistics([FromQuery] int childId, [FromQuery] DateOnly startDay, [FromQuery] DateOnly endDay)
        {
            return await _service.GetStatisticsAsync(childId, startDay, endDay);
        }
    }
}
