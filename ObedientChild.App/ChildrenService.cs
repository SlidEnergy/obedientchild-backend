using Microsoft.EntityFrameworkCore;
using ObedientChild.App.Balance;
using ObedientChild.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObedientChild.App
{
    public class ChildrenService : IChildrenService
	{
		private readonly IApplicationDbContext _context;
        private readonly IBalanceHistoryFactory _balanceHistoryFactory;
        private readonly IBalanceService _balanceService;

        public ChildrenService(IApplicationDbContext context, 
            IBalanceHistoryFactory historyFactory, IBalanceService balanceService)
        {
            _context = context;
            _balanceHistoryFactory = historyFactory;
            _balanceService = balanceService;
        }

        public async Task<List<Child>> GetListAsync()
		{
			return await _context.Children.ToListAsync();
		}

		public async Task<ChildView> GetByIdAsync(int childId)
		{
			var child = await _context.Children.FindAsync(childId);
			var statuses = await _context.ChildStatuses.Where(x => x.ChildId == childId).ToListAsync();

            return new ChildView(child) { Statuses = statuses};
		}

        public async Task SaveAvatarAsync(int childId, byte[] data)
        {
            var child = await _context.Children.FindAsync(childId);

            if (child != null)
            {
                child.Avatar = data;

                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> EarnCountAsync(int childId, int count)
        {
            var child = await _context.Children.FindAsync(childId);

            if (child != null)
            {
                var history = _balanceHistoryFactory.Create(childId, count);
                await _balanceService.EarnCoinAsync(child, count, history.CloneProps());

                return child.Balance;
            }

            return 0;
        }

        public async Task<int> SpendCountAsync(int childId, int count)
        {
            var child = await _context.Children.FindAsync(childId);

            if (child != null)
            {
                var history = _balanceHistoryFactory.Create(childId, count, true);
                await _balanceService.SpendCoinAsync(child, count, history.CloneProps());

                return child.Balance;
            }

            return 0;
        }

        public async Task SetGoalAsync(int id, int rewardId)
        {
            var child = await _context.Children.FindAsync(id);
            var reward = await _context.Deeds.SingleOrDefaultAsync(x => x.Id == rewardId && x.Type == DeedType.Reward);

            if (child != null && reward != null)
            {
                child.SetBigGoal(rewardId);

                await _context.SaveChangesAsync();
            }
        }

        public async Task SetDreamAsync(int id, int rewardId)
        {
            var child = await _context.Children.FindAsync(id);
            var reward = await _context.Deeds.SingleOrDefaultAsync(x => x.Id == rewardId && x.Type == DeedType.Reward);

            if (child != null && reward != null)
            {
                child.SetDream(rewardId);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<ChildStatus> AddStatusAsync(int id, ChildStatus childStatus)
        {
            var child = await _context.Children.FindAsync(id);

            if (child != null)
            {
                childStatus.ChildId = id;
                _context.ChildStatuses.Add(childStatus);

                await _context.SaveChangesAsync();

                return childStatus;
            }

            return null;
        }

        public async Task DeleteStatusAsync(int id, int childStatusId)
        {
            var child = await _context.Children.FindAsync(id);
            var model = await _context.ChildStatuses.FindAsync(childStatusId);

            if (child != null && model != null)
            {
                _context.ChildStatuses.Remove(model);

                await _context.SaveChangesAsync();
            }
        }
    }
}
