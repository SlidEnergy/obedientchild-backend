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
    public class HabitsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHabitsService _service;

        public HabitsController(IMapper mapper, IHabitsService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet("day")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<DayHabit>>> GetListForDay([FromQuery] int childId, [FromQuery] DateOnly day)
        {
            // получаем объединенный список привычек активных на текущий момент + выполненные или пропущенные
            var list = await _service.GetListForDayAsync(childId, day);

            return list;
        }

        [HttpPost("{habitId}/child/{childId}")]
        public async Task SetForChild(int habitId, int childId)
        {
            if (ModelState.IsValid)
            {
                await _service.SetForChildAsync(childId, habitId);
            }
        }

        [HttpDelete("{habitId}/child/{childId}")]
        public async Task UnsetForChild(int habitId, int childId, [FromQuery] DateOnly day)
        {
            await _service.UnsetForChildAsync(habitId, childId, day);
        }

        [HttpPost("{id}/status")]
        public async Task<ActionResult<HabitHistory>> SetStatus(int id, [FromQuery] int childId, [FromQuery] DateOnly day, [FromQuery] HabitHistoryStatus status)
        {
            var userId = User.GetUserId();

            return await _service.SetStatusAsync(id, childId, day, status, userId);
        }

        [HttpGet("statistics")]
        public async Task<ActionResult<WeekHabitStatistic>> GetStatistics([FromQuery] int childId, [FromQuery] DateOnly startDay, [FromQuery] DateOnly endDay)
        {
            return await _service.GetStatisticsAsync(childId, startDay, endDay);
        }
    }
}
