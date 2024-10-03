using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ObedientChild.App.Balance;
using ObedientChild.Domain;
using ObedientChild.Domain.Habits;
using ObedientChild.Domain.Tasks;

namespace ObedientChild.App.Habits
{
    public class ChildTasksService : IChildTasksService
    {
        private readonly IApplicationDbContext _context;
        private readonly IBalanceHistoryFactory _historyFactory;
        private readonly IBalanceService _balanceService;

        public ChildTasksService(IApplicationDbContext context, IBalanceHistoryFactory historyFactory, IBalanceService balanceService)
        {
            _context = context;
            _historyFactory = historyFactory;
            _balanceService = balanceService;
        }

        public async Task<List<ChildTask>> GetListAsync()
        {
            return await _context.ChildTasks.ToListAsync();
        }

        public async Task<List<ChildTask>> GetListForDayAsync(int childId, DateOnly day)
        {
            return await _context.ChildTasks.Include(x => x.Deed)
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

        public async Task<ChildTask> SetStatusAsync(string userId, int childTaskId, int childId, ChildTaskStatus status)
        {
            var child = await _context.Children.FindAsync(childId);

            if (child == null)
                return null;

            var childTask = await _context.ChildTasks.Include(x => x.Deed).ThenInclude(x => x.CharacterTraitDeeds).SingleOrDefaultAsync(x => x.Id == childTaskId);

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
                var coinHistory = _historyFactory.Create(childId, childTask.Deed, true);
                await _balanceService.SpendCoinAsync(child, childTask.Deed.Price, coinHistory.CloneProps());

                if (childTask.Deed.CharacterTraitIds != null)
                    await _balanceService.LoseExperienceAsync(childId, childTask.Deed.CharacterTraitIds, childTask.Deed.Price, coinHistory.CloneProps());
            }

            if ((childTask.Status == ChildTaskStatus.Failed && status == ChildTaskStatus.ToDo) ||
                (childTask.Status == ChildTaskStatus.ToDo && status == ChildTaskStatus.Done))
            {
                var coinHistory = _historyFactory.Create(childId, childTask.Deed);
                await _balanceService.EarnCoinAsync(child, childTask.Deed.Price, coinHistory.CloneProps());

                if(childTask.Deed.CharacterTraitIds != null)
                    await _balanceService.AddExperienceAsync(childId, childTask.Deed.CharacterTraitIds, childTask.Deed.Price, coinHistory.CloneProps());

                await _balanceService.PowerDownLifeEnergyAsync(userId, childTask.Deed.Price, coinHistory.CloneProps());
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
