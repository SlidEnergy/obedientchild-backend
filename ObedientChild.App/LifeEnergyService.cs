using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ObedientChild.App.Balance;
using ObedientChild.Domain;
using ObedientChild.Domain.LifeEnergy;

namespace ObedientChild.App
{
    public class LifeEnergyService : ILifeEnergyService
    {
        private readonly IApplicationDbContext _context;
        private readonly IBalanceHistoryFactory _balanceHistoryFactory;
        private readonly IBalanceService _balanceService;

        public LifeEnergyService(IApplicationDbContext context, IBalanceHistoryFactory balanceHistoryFactory, IBalanceService balanceService)
        {
            _context = context;
            _balanceHistoryFactory = balanceHistoryFactory;
            _balanceService = balanceService;
        }

        public async Task<LifeEnergyAccount> GetAccountWithAccessCheckAsync(string userId)
        {
            return await _context.GetLifeEnergyAccountWithAccessCheckAsync(userId);
        }

        public async Task PowerUpAsync(string userId, int amount, string title)
        {
            var account = await _context.GetLifeEnergyAccountWithAccessCheckAsync(userId);

            if (account == null)
                return;

            var logEntry = _balanceHistoryFactory.Create(userId, amount, false, title);
            await _balanceService.PowerUpLifeEnergyAsync(userId, amount, logEntry.CloneProps());
        }

        public async Task PowerDownAsync(string userId, int amount, string title)
        {
            var account = await _context.GetLifeEnergyAccountWithAccessCheckAsync(userId);

            if (account == null)
                return;

            var logEntry = _balanceHistoryFactory.Create(userId, amount, true, title);
            await _balanceService.PowerDownLifeEnergyAsync(userId, amount, logEntry.CloneProps());
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
