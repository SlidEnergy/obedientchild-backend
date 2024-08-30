using ObedientChild.Domain;
using ObedientChild.Domain.Habits;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ObedientChild.App.Habits
{
    public interface IHabitsService
    {
        System.Threading.Tasks.Task AddAsync(Habit model);
        System.Threading.Tasks.Task SetForChildAsync(int childId, int habitId);
        System.Threading.Tasks.Task UnsetForChildAsync(int id, int childId, DateOnly day);
        System.Threading.Tasks.Task DeleteAsync(int id);
        Task<Habit> GetByIdAsync(int id);
        Task<List<Habit>> GetListAsync();
        Task<List<DayHabit>> GetListForDayAsync(int childId, DateOnly day);
        Task<Habit> UpdateAsync(Habit model);

        Task<HabitHistory> SetStatusAsync(int habitId, int childId, DateOnly day, HabitHistoryStatus status);
        Task<WeekHabitStatistic> GetStatisticsAsync(int childId, DateOnly startDay, DateOnly endDay);
    }
}