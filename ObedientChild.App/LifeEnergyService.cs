using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ObedientChild.Domain;
using ObedientChild.Domain.LifeEnergy;

namespace ObedientChild.App
{
    public class LifeEnergyService : ILifeEnergyService
    {
        private readonly IApplicationDbContext _context;

        public LifeEnergyService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LifeEnergyAccount> GetAccountWithAccessCheckAsync(string userId)
        {
            return await _context.GetLifeEnergyAccountWithAccessCheckAsync(userId);
        }

        public async Task<LifeEnergyHistory> PowerUpAsync(string userId, int amount, string title)
        {
            var account = await _context.GetLifeEnergyAccountWithAccessCheckAsync(userId);

            if (account == null)
                return null;

            account.Balance += amount;

            var history = new LifeEnergyHistory(amount, title, account.Id);

            _context.LifeEnergyHistory.Add(history);

            await _context.SaveChangesAsync();

            return history;
        }

        public async Task<LifeEnergyHistory> PowerDownAsync(string userId, int amount, string title)
        {
            var account = await _context.GetLifeEnergyAccountWithAccessCheckAsync(userId);

            if (account == null)
                return null;

            account.Balance -= amount;

            var history = new LifeEnergyHistory(-1 * amount, title, account.Id);

            _context.LifeEnergyHistory.Add(history);

            await _context.SaveChangesAsync();

            return history;
        }

        public async Task<List<LifeEnergyHistory>> GetHistoryListWithAccessCheckAsync(string userId)
        {
            return await _context.Users
                .Where(x => x.Id == userId)
                .Join(_context.TrusteeLifeEnergyAccounts, u => u.TrusteeId, t => t.TrusteeId, (u, t) => t)
                .Join(_context.LifeEnergyHistory, t => t.LifeEnergyAccountId, h => h.LifeEnergyAccountId, (t, h) => h)
                .ToListAsync();
        }

        public async Task<LifeEnergyHistory> GetHistoryByIdAsync(string userId, int id)
        {
            return await _context.GetLifeEnergyHistoryWithAccessCheckAsync(userId, id);
        }

        public async Task RevertHistoryAsync(string userId, int id)
        {
            var history = await _context.GetLifeEnergyHistoryWithAccessCheckAsync(userId, id);

            if (history == null)
                return;

            var lifeEnergyAccount = await _context.Children.FindAsync(history.LifeEnergyAccountId);

            if (lifeEnergyAccount == null)
                return;

            lifeEnergyAccount.Balance -= history.Amount;

            _context.LifeEnergyHistory.Remove(history);
            await _context.SaveChangesAsync();
        }

        public async Task<LifeEnergyAccount> CreateAccountAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
                return null;

            var account = await _context.GetLifeEnergyAccountWithAccessCheckAsync(userId);

            if (account != null)
                return null;

            var lifeEnergyAccount = new LifeEnergyAccount();

            _context.LifeEnergyAccounts.Add(lifeEnergyAccount);

            _context.TrusteeLifeEnergyAccounts.Add(new TrusteeLifeEnergyAccount(user, lifeEnergyAccount));

            await _context.SaveChangesAsync();

            return lifeEnergyAccount;
        }

        public async Task RemoveAccountAsync(string userId)
        {
            var account = await _context.GetLifeEnergyAccountWithAccessCheckAsync(userId);

            if (account == null)
                return;

            _context.LifeEnergyAccounts.Remove(account);

            await _context.SaveChangesAsync();
        }
    }
}
