using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ObedientChild.Domain;

namespace ObedientChild.App
{
    public class BalanceHistoryService : IBalanceHistoryService
    {
        private readonly IApplicationDbContext _context;

        public BalanceHistoryService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<BalanceHistory>> GetListAsync(int childId, BalanceType type)
        {
            if (childId == 0)
                return await _context.BalanceHistory.Where(x => x.BalanceType == type).ToListAsync();

            // TODO: add index by childId

            return await _context.BalanceHistory.Where(x => x.EntityId == childId && x.BalanceType == type).ToListAsync();
        }

        public async Task<BalanceHistory> GetByIdAsync(int id)
        {
            return await _context.BalanceHistory.FindAsync(id);
        }

        public async Task RevertAsync(int id)
        {
            var history = await _context.BalanceHistory.FindAsync(id);

            if (history == null)
                return;

            var child = await _context.Children.FindAsync(history.EntityId);

            if (child == null)
                return;

            if (history.BalanceType == BalanceType.CoinBalance)
                child.Balance -= history.Amount;

            if (history.BalanceType == BalanceType.LifeEnergyBalance)
            {
                var account = await _context.LifeEnergyAccounts.SingleOrDefaultAsync(x => x.Id == history.EntityId);
                account.PowerDown(history.Amount);
            }

            if (history.BalanceType == BalanceType.Experience)
            {
                var childTrait = await _context.ChildCharacterTraits.SingleOrDefaultAsync(x => x.Id == history.EntityId);
                childTrait.LoseExperience(history.Amount);
            }

            _context.BalanceHistory.Remove(history);
            await _context.SaveChangesAsync();
        }
    }
}
