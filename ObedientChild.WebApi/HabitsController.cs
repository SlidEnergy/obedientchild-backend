using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ObedientChild.App;
using ObedientChild.App.Habits;
using ObedientChild.Domain;
using ObedientChild.Domain.Habits;

namespace ObedientChild.WebApi
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class HabitsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHabitsService _service;

        public HabitsController(IMapper mapper, IHabitsService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Habit>>> GetList()
        {
            var list = await _service.GetListAsync();

            return list;
        }

        [HttpGet("day")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<DayHabit>>> GetListForDay([FromQuery] int childId, [FromQuery] DateOnly day)
        {
            // получаем объединенный список привычек активных на текущий момент + выполненные или пропущенные
            var list = await _service.GetListForDayAsync(childId, day);

            return list;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Habit>> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);

            return item;
        }

        [HttpPut]
        public async System.Threading.Tasks.Task Add(Habit habit)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(habit);
            }
        }

        [HttpPost("{habitId}/child/{childId}")]
        public async System.Threading.Tasks.Task SetForChild(int habitId, int childId)
        {
            if (ModelState.IsValid)
            {
                await _service.SetForChildAsync(childId, habitId);
            }
        }


        [HttpDelete("{id}")]
        public async System.Threading.Tasks.Task Delete(int id)
        {
            // set end date or remove if doesn't exist in habit history
             await _service.DeleteAsync(id);
        }

        [HttpDelete("{habitId}/child/{childId}")]
        public async System.Threading.Tasks.Task UnsetForChild(int habitId, int childId, [FromQuery] DateOnly day)
        {
            await _service.UnsetForChildAsync(habitId, childId, day);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<Habit>> Update(int id, [FromBody] Habit model)
        {
            return await _service.UpdateAsync(model);
        }

        [HttpPost("{id}/status")]
        public async Task<ActionResult<HabitHistory>> SetStatus(int id, [FromQuery] int childId, [FromQuery] DateOnly day, [FromQuery] HabitHistoryStatus status)
        {
            return await _service.SetStatusAsync(id, childId, day, status);
        }

        [HttpGet("statistics")]
        public async Task<ActionResult<WeekHabitStatistic>> GetStatistics([FromQuery] int childId, [FromQuery] DateOnly startDay, [FromQuery] DateOnly endDay)
        {
            return await _service.GetStatisticsAsync(childId, startDay, endDay);
        }
    }
}
