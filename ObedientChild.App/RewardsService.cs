using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ObedientChild.Domain;

namespace ObedientChild.App
{
    public class RewardsService : IRewardsService
	{
		private readonly IApplicationDbContext _context;

		public RewardsService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Reward>> GetListAsync()
		{
			return await _context.Rewards.ToListAsync();
		}

		public async Task<Reward> GetByIdAsync(int id)
		{
			return await _context.Rewards.FindAsync(id);
		}

        public async System.Threading.Tasks.Task AddRewardAsync(Reward reward)
        {
            _context.Rewards.Add(reward);

            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteRewardAsync(int id)
        {
            var reward = await _context.Rewards.FindAsync(id);

            if (reward != null)
            {
                _context.Rewards.Remove(reward);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Reward> UpdateAsync(Reward reward)
        {
            _context.Entry(reward).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return reward;
        }
    }
}
