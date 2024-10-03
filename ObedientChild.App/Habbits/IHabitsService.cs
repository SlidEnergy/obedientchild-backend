using ObedientChild.Domain;
using ObedientChild.Domain.Habits;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ObedientChild.App.Habits
{
    public interface IHabitsService
    {
        Task SetForChildAsync(int childId, int habitId);
        Task UnsetForChildAsync(int id, int childId, DateOnly day);
        Task<List<DayHabit>> GetListForDayAsync(int childId, DateOnly day);
        Task<HabitHistory> SetStatusAsync(int habitId, int childId, DateOnly day, HabitHistoryStatus status, string userId = null);
        Task<WeekHabitStatistic> GetStatisticsAsync(int childId, DateOnly startDay, DateOnly endDay);
    }
}