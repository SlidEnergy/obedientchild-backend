using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ObedientChild.Domain;
using ObedientChild.Domain.Habits;
using ObedientChild.Domain.Tasks;

namespace ObedientChild.App.Habits
{
    public class ChildTasksService : IChildTasksService
    {
        private readonly IApplicationDbContext _context;
        private readonly ICoinHistoryFactory _historyFactory;

        public ChildTasksService(IApplicationDbContext context, ICoinHistoryFactory historyFactory)
        {
            _context = context;
            _historyFactory = historyFactory;
        }

        public async Task<List<ChildTask>> GetListAsync()
        {
            return await _context.ChildTasks.ToListAsync();
        }

        public async Task<List<ChildTask>> GetListForDayAsync(int childId, DateOnly day)
        {
            return await _context.ChildTasks.Include(x => x.GoodDeed)
                .Where(x => x.ChildId == childId && day == x.Date)
                .ToListAsync();
        }

        public async Task<ChildTask> GetByIdAsync(int id)
        {
            return await _context.ChildTasks.FindAsync(id);
        }

        public async Task AddAsync(ChildTask model)
        {
            _context.ChildTasks.Add(model);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var model = await _context.ChildTasks.FindAsync(id);

            if (model != null)
            {
                _context.ChildTasks.Remove(model);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ChildTask> SetStatusAsync(int id, int childId, ChildTaskStatus status)
        {
            var child = await _context.Children.FindAsync(childId);

            if (child == null)
                return null;

            var childTask = await _context.ChildTasks.SingleOrDefaultAsync(x => x.Id == id);

            if (childTask == null)
                return null;

            if (childTask.Status == status)
                return null;

            if (childTask.Status != ChildTaskStatus.ToDo && status != ChildTaskStatus.ToDo)
                throw new InvalidOperationException("Doesn't supports transit status between Done and Failed. Change status to ToDo first.");

            // Rollback coin history and spend/earn
            if ((childTask.Status == ChildTaskStatus.Done && status == ChildTaskStatus.ToDo) ||
                (childTask.Status == ChildTaskStatus.ToDo && status == ChildTaskStatus.Failed))
            {
                var coinHistory = _historyFactory.CreateSpendGoodDeed(childId, childTask.GoodDeed);
                _context.CoinHistory.Add(coinHistory);

                child.SpendCoin(childTask.GoodDeed.Price);
            }

            if ((childTask.Status == ChildTaskStatus.Failed && status == ChildTaskStatus.ToDo) ||
                (childTask.Status == ChildTaskStatus.ToDo && status == ChildTaskStatus.Done))
            {
                var coinHistory = _historyFactory.CreateEarnGoodDeed(childId, childTask.GoodDeed);
                _context.CoinHistory.Add(coinHistory);

                child.EarnCoin(childTask.GoodDeed.Price);
                await _context.SaveChangesAsync();
            }

            childTask.Status = status;
            await _context.SaveChangesAsync();
            return childTask;
        }

        public async Task<ChildTask> UpdateAsync(ChildTask model)
        {
            _context.Entry(model).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return model;
        }
    }
}
