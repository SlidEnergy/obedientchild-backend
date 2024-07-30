using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ObedientChild.Domain;
using ObedientChild.Domain.Habbits;

namespace ObedientChild.App.Habbits
{
    public class HabbitsService : IHabbitsService
    {
        private readonly IApplicationDbContext _context;
        private readonly ICoinHistoryFactory _historyFactory;

        public HabbitsService(IApplicationDbContext context, ICoinHistoryFactory historyFactory)
        {
            _context = context;
            _historyFactory = historyFactory;
        }

        public async Task<List<Habbit>> GetListAsync()
        {
            return await _context.Habbits.ToListAsync();
        }

        public async Task<List<DayHabbit>> GetListForDayAsync(int childId, DateOnly day)
        {
            var habbits = await _context.ChildHabbits
                .Where(x => x.ChildId == childId && day >= x.StartDate && x.EndDate == null)
                .Join(_context.Habbits, ch => ch.HabbitId, h => h.Id, (ch, h) => h)
                .Select(x => new DayHabbit(day, x, HabbitHistoryStatus.None))
                .ToListAsync();

            var history = await _context.HabbitHistory
                .Where(x => x.ChildId == childId && x.Day == day)
                .Join(_context.Habbits, hh => hh.HabbitId, h => h.Id, (hh, h) => new { HabbitHistory = hh, Habbit = h })
                .Select(x => new DayHabbit(day, x.Habbit, x.HabbitHistory.Status))
                .ToListAsync();

            return history.UnionBy(habbits, x => x.HabbitId).ToList();
        }

        public async Task<Habbit> GetByIdAsync(int id)
        {
            return await _context.Habbits.FindAsync(id);
        }

        public async Task AddAsync(Habbit model)
        {
            _context.Habbits.Add(model);

            await _context.SaveChangesAsync();
        }

        public async Task SetForChildAsync(int childId, int habbitId)
        {
            var model = await _context.ChildHabbits.FirstOrDefaultAsync(x => x.HabbitId == habbitId && x.ChildId == childId);

            if (model != null)
            {
                model.EndDate = null;
            }
            else
            {
                _context.ChildHabbits.Add(new ChildHabbit(childId, habbitId));
            }

            await _context.SaveChangesAsync();
        }

        public async Task UnsetForChildAsync(int id, int childId)
        {
            var model = await _context.ChildHabbits.FirstOrDefaultAsync(x => x.HabbitId == id && x.ChildId == childId);

            if (model != null)
            {
                model.EndDate = DateOnly.FromDateTime(DateTime.Today);

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var model = await _context.Habbits.FindAsync(id);

            if (model != null)
            {
                _context.Habbits.Remove(model);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<HabbitHistory> SetStatusAsync(int habbitId, int childId, DateOnly day, HabbitHistoryStatus status)
        {
            var habbit = await _context.Habbits.FirstOrDefaultAsync(x => x.Id == habbitId);

            if (habbit == null)
                return null;

            var habbitHistory = await _context.HabbitHistory.FirstOrDefaultAsync(x => x.HabbitId == habbitId && x.ChildId == childId && x.Day == day);

            // update or remove entry
            if (habbitHistory != null)
            {
                if (status == HabbitHistoryStatus.None)
                {
                    var coinHistory = _historyFactory.CreateSpendHabbit(childId, habbit);
                    _context.CoinHistory.Add(coinHistory);

                    _context.HabbitHistory.Remove(habbitHistory);
                    await _context.SaveChangesAsync();

                    return null;
                }
                else if (habbitHistory.Status != status)
                {
                    if (status == HabbitHistoryStatus.Done)
                    {
                        var coinHistory = _historyFactory.CreateEarnHabbit(childId, habbit);
                        _context.CoinHistory.Add(coinHistory);
                    }

                    habbitHistory.Status = status;
                    await _context.SaveChangesAsync();

                    return habbitHistory;
                }
            }
            // create entry
            else
            {
                var model = await _context.ChildHabbits.FirstOrDefaultAsync(x => x.HabbitId == habbitId && x.ChildId == childId);

                if (model != null)
                {
                    if (status == HabbitHistoryStatus.None)
                        return null;

                    var coinHistory = _historyFactory.CreateEarnHabbit(childId, habbit);
                    _context.CoinHistory.Add(coinHistory);

                    var newHabbitHistory = new HabbitHistory(day, childId, habbitId, status);
                    _context.HabbitHistory.Add(newHabbitHistory);
                    await _context.SaveChangesAsync();
                    
                    return newHabbitHistory;
                }
            }

            return null;
        }

        public async Task<Habbit> UpdateAsync(Habbit model)
        {
            _context.Entry(model).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<WeekHabbitStatistic> GetStatisticsAsync(int childId, DateOnly startDay, DateOnly endDay)
        {
            var result = new WeekHabbitStatistic();

            int totalHabbits = 0;
            int totalSkipped = 0;
            int totalDone = 0;

            DateOnly day = startDay;
            while (day <= endDay)
            {
                var habbitsCount = await _context.ChildHabbits
                   .Where(x => x.ChildId == childId && day >= x.StartDate && x.EndDate == null)
                   .CountAsync();

                var history = await _context.HabbitHistory
                    .Where(x => x.ChildId == childId && x.Day == day)
                    .ToListAsync();

                var dayStatistic = new DayStatistic()
                {
                    Day = day,
                    HabbitsCount = habbitsCount,
                    DoneHabbitsCount = history.Count(x => x.Status == HabbitHistoryStatus.Done),
                    SkippedHabbitsCount = history.Count(x => x.Status == HabbitHistoryStatus.Skpped),
                };

                result.DayStatistics.Add(dayStatistic);

                totalHabbits += habbitsCount;
                totalDone += dayStatistic.DoneHabbitsCount;
                totalSkipped += dayStatistic.SkippedHabbitsCount;

                day = day.AddDays(1);
            }

            result.HabbitsCount = totalHabbits; 
            result.DoneHabbitsCount = totalDone; 
            result.SkippedHabbitsCount = totalSkipped; 

            return result;
        }
    }
}
