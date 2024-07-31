using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ObedientChild.Domain;
using ObedientChild.Domain.Habits;

namespace ObedientChild.App.Habits
{
    public class HabitsService : IHabitsService
    {
        private readonly IApplicationDbContext _context;
        private readonly ICoinHistoryFactory _historyFactory;

        public HabitsService(IApplicationDbContext context, ICoinHistoryFactory historyFactory)
        {
            _context = context;
            _historyFactory = historyFactory;
        }

        public async Task<List<Habit>> GetListAsync()
        {
            return await _context.Habits.ToListAsync();
        }

        public async Task<List<DayHabit>> GetListForDayAsync(int childId, DateOnly day)
        {
            var habits = await _context.ChildHabits
                .Where(x => x.ChildId == childId && day >= x.StartDate && x.EndDate == null)
                .Join(_context.Habits, ch => ch.HabitId, h => h.Id, (ch, h) => h)
                .Select(x => new DayHabit(day, x, HabitHistoryStatus.None))
                .ToListAsync();

            var history = await _context.HabitHistory
                .Where(x => x.ChildId == childId && x.Day == day)
                .Join(_context.Habits, hh => hh.HabitId, h => h.Id, (hh, h) => new { HabitHistory = hh, Habit = h })
                .Select(x => new DayHabit(day, x.Habit, x.HabitHistory.Status))
                .ToListAsync();

            return history.UnionBy(habits, x => x.HabitId).ToList();
        }

        public async Task<Habit> GetByIdAsync(int id)
        {
            return await _context.Habits.FindAsync(id);
        }

        public async Task AddAsync(Habit model)
        {
            _context.Habits.Add(model);

            await _context.SaveChangesAsync();
        }

        public async Task SetForChildAsync(int childId, int habitId)
        {
            var model = await _context.ChildHabits.FirstOrDefaultAsync(x => x.HabitId == habitId && x.ChildId == childId);

            if (model != null)
            {
                model.EndDate = null;
            }
            else
            {
                _context.ChildHabits.Add(new ChildHabit(childId, habitId));
            }

            await _context.SaveChangesAsync();
        }

        public async Task UnsetForChildAsync(int id, int childId, DateOnly day)
        {
            var model = await _context.ChildHabits.FirstOrDefaultAsync(x => x.HabitId == id && x.ChildId == childId);

            if (model != null)
            {
                model.EndDate = day;
            }

            var history = await _context.HabitHistory.FirstOrDefaultAsync(x => x.Day == day && x.HabitId == id && x.ChildId == childId);

            if (history != null)
            {
                _context.HabitHistory.Remove(history);
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var model = await _context.Habits.FindAsync(id);

            if (model != null)
            {
                _context.Habits.Remove(model);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<HabitHistory> SetStatusAsync(int habitId, int childId, DateOnly day, HabitHistoryStatus status)
        {
            var habit = await _context.Habits.FirstOrDefaultAsync(x => x.Id == habitId);

            if (habit == null)
                return null;

            var habitHistory = await _context.HabitHistory.FirstOrDefaultAsync(x => x.HabitId == habitId && x.ChildId == childId && x.Day == day);

            // update or remove entry
            if (habitHistory != null)
            {
                if (status == HabitHistoryStatus.None)
                {
                    var coinHistory = _historyFactory.CreateSpendHabit(childId, habit);
                    _context.CoinHistory.Add(coinHistory);

                    _context.HabitHistory.Remove(habitHistory);
                    await _context.SaveChangesAsync();

                    return null;
                }
                else if (habitHistory.Status != status)
                {
                    if (status == HabitHistoryStatus.Done)
                    {
                        var coinHistory = _historyFactory.CreateEarnHabit(childId, habit);
                        _context.CoinHistory.Add(coinHistory);
                    }

                    habitHistory.Status = status;
                    await _context.SaveChangesAsync();

                    return habitHistory;
                }
            }
            // create entry
            else
            {
                var model = await _context.ChildHabits.FirstOrDefaultAsync(x => x.HabitId == habitId && x.ChildId == childId);

                if (model != null)
                {
                    if (status == HabitHistoryStatus.None)
                        return null;

                    var coinHistory = _historyFactory.CreateEarnHabit(childId, habit);
                    _context.CoinHistory.Add(coinHistory);

                    var newHabitHistory = new HabitHistory(day, childId, habitId, status);
                    _context.HabitHistory.Add(newHabitHistory);
                    await _context.SaveChangesAsync();
                    
                    return newHabitHistory;
                }
            }

            return null;
        }

        public async Task<Habit> UpdateAsync(Habit model)
        {
            _context.Entry(model).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<WeekHabitStatistic> GetStatisticsAsync(int childId, DateOnly startDay, DateOnly endDay)
        {
            var result = new WeekHabitStatistic();

            int totalHabits = 0;
            int totalSkipped = 0;
            int totalDone = 0;
            int totalFailed = 0;

            DateOnly day = startDay;
            while (day <= endDay)
            {
                var habitsCount = await _context.ChildHabits
                   .Where(x => x.ChildId == childId && day >= x.StartDate && x.EndDate == null)
                   .CountAsync();

                var history = await _context.HabitHistory
                    .Where(x => x.ChildId == childId && x.Day == day)
                    .ToListAsync();

                var dayStatistic = new DayStatistic()
                {
                    Day = day,
                    HabitsCount = habitsCount,
                    DoneHabitsCount = history.Count(x => x.Status == HabitHistoryStatus.Done),
                    SkippedHabitsCount = history.Count(x => x.Status == HabitHistoryStatus.Skipped),
                    FailedHabitsCount = history.Count(x => x.Status == HabitHistoryStatus.Failed),
                };

                result.DayStatistics.Add(dayStatistic);

                totalHabits += habitsCount;
                totalDone += dayStatistic.DoneHabitsCount;
                totalSkipped += dayStatistic.SkippedHabitsCount;
                totalFailed += dayStatistic.FailedHabitsCount;

                day = day.AddDays(1);
            }

            result.HabitsCount = totalHabits; 
            result.DoneHabitsCount = totalDone; 
            result.SkippedHabitsCount = totalSkipped; 
            result.FailedHabitsCount = totalFailed; 

            return result;
        }
    }
}
