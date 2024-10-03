using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ObedientChild.App.Balance;
using ObedientChild.Domain;
using ObedientChild.Domain.Habits;

namespace ObedientChild.App.Habits
{
    public class HabitsService : IHabitsService
    {
        private readonly IApplicationDbContext _context;
        private readonly IBalanceHistoryFactory _historyFactory;
        private readonly IBalanceService _balanceService;

        public HabitsService(IApplicationDbContext context, IBalanceHistoryFactory historyFactory, IBalanceService balanceService)
        {
            _context = context;
            _historyFactory = historyFactory;
            _balanceService = balanceService;
        }

        public async Task<List<DayHabit>> GetListForDayAsync(int childId, DateOnly day)
        {
            var habits = await _context.ChildHabits
                .Where(x => x.ChildId == childId && day >= x.StartDate && x.EndDate == null)
                .Join(_context.Deeds, ch => ch.DeedId, h => h.Id, (ch, h) => h)
                .Select(x => new DayHabit(day, x, HabitHistoryStatus.None))
                .ToListAsync();

            var history = await _context.HabitHistory
                .Where(x => x.ChildId == childId && x.Day == day)
                .Join(_context.Deeds, hh => hh.DeedId, h => h.Id, (hh, h) => new { HabitHistory = hh, Habit = h })
                .Select(x => new DayHabit(day, x.Habit, x.HabitHistory.Status))
                .ToListAsync();

            return history.UnionBy(habits, x => x.HabitId).ToList();
        }

        public async Task SetForChildAsync(int childId, int habitId)
        {
            var model = await _context.ChildHabits.FirstOrDefaultAsync(x => x.DeedId == habitId && x.ChildId == childId);

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
            var model = await _context.ChildHabits.FirstOrDefaultAsync(x => x.DeedId == id && x.ChildId == childId);

            if (model != null)
            {
                model.EndDate = day;
            }

            var history = await _context.HabitHistory.FirstOrDefaultAsync(x => x.Day == day && x.DeedId == id && x.ChildId == childId);

            if (history != null)
            {
                _context.HabitHistory.Remove(history);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<HabitHistory> SetStatusAsync(int habitId, int childId, DateOnly day, HabitHistoryStatus status, string userId = null)
        {
            var child = await _context.Children.FindAsync(childId);

            var habit = await _context.Deeds.Include(x => x.CharacterTraitDeeds).FirstOrDefaultAsync(x => x.Id == habitId);

            if (habit == null)
                return null;

            var habitHistory = await _context.HabitHistory.FirstOrDefaultAsync(x => x.DeedId == habitId && x.ChildId == childId && x.Day == day);

            // update or remove entry
            if (habitHistory != null)
            {
                if (habitHistory.Status == status)
                    return null;

                if (habitHistory.Status == HabitHistoryStatus.None)
                    throw new InvalidOperationException("Doesn't support operations habbit history with status None");

                if (status != HabitHistoryStatus.None)
                    throw new InvalidOperationException("If habit history exists support only transit status to None");

                // Rollback coin history and spend/earn
                if (habitHistory.Status == HabitHistoryStatus.Done)
                {
                    var logEntry = _historyFactory.Create(childId, habit, true);

                    await _balanceService.SpendCoinAsync(child, habit.Price, logEntry.CloneProps());

                    if (habit.CharacterTraitIds != null)
                        await _balanceService.LoseExperienceAsync(childId, habit.CharacterTraitIds, habit.Price, logEntry.CloneProps());

                    if(userId != null)
                        await _balanceService.PowerUpLifeEnergyAsync(userId, habit.Price, logEntry.CloneProps());
                }

                if (habitHistory.Status == HabitHistoryStatus.Failed)
                {
                    var logEntry = _historyFactory.Create(childId, habit);

                    await _balanceService.EarnCoinAsync(child, habit.Price, logEntry.CloneProps());
                }

                _context.HabitHistory.Remove(habitHistory);
                await _context.SaveChangesAsync();

                return null;
            }
            // create entry
            else
            {
                var model = await _context.ChildHabits.FirstOrDefaultAsync(x => x.DeedId == habitId && x.ChildId == childId);

                if (model != null)
                {
                    if (status == HabitHistoryStatus.Done)
                    {
                        var logEntry = _historyFactory.Create(childId, habit);

                        await _balanceService.EarnCoinAsync(child, habit.Price, logEntry.CloneProps());

                        if (habit.CharacterTraitIds != null)
                            await _balanceService.AddExperienceAsync(childId, habit.CharacterTraitIds, habit.Price, logEntry.CloneProps());

                        if(userId != null)
                            await _balanceService.PowerDownLifeEnergyAsync(userId, habit.Price, logEntry.CloneProps());
                    }

                    if (status == HabitHistoryStatus.Failed)
                    {
                        var logEntry = _historyFactory.Create(childId, habit, true);

                        await _balanceService.SpendCoinAsync(child, habit.Price, logEntry.CloneProps());
                    }

                    var newHabitHistory = new HabitHistory(day, childId, habitId, status);
                    _context.HabitHistory.Add(newHabitHistory);
                    await _context.SaveChangesAsync();

                    return newHabitHistory;
                }
            }

            return null;
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
