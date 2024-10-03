using Microsoft.EntityFrameworkCore;
using ObedientChild.App.Balance;
using ObedientChild.Domain;
using ObedientChild.Domain.Personalities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ObedientChild.App
{
    public class BalanceService : IBalanceService
    {
        private readonly IApplicationDbContext _context;

        public BalanceService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task EarnCoinAsync(Child child, int count, BalanceHistoryProps logEntry)
        {
            child.EarnCoin(count);

            logEntry.BalanceType = BalanceType.CoinBalance;
            _context.BalanceHistory.Add(logEntry.ToModel());

            await _context.SaveChangesAsync();
        }

        public async Task SpendCoinAsync(Child child, int count, BalanceHistoryProps logEntry)
        {
            child.SpendCoin(count);

            logEntry.BalanceType = BalanceType.CoinBalance;
            _context.BalanceHistory.Add(logEntry.ToModel());

            await _context.SaveChangesAsync();
        }

        public async Task PowerUpLifeEnergyAsync(string userId, int count, BalanceHistoryProps logEntry)
        {
            var account = await _context.GetLifeEnergyAccountWithAccessCheckAsync(userId);

            if (account == null)
                return;

            account.PowerUp(count);

            logEntry.EntityId = account.Id;
            logEntry.BalanceType = BalanceType.LifeEnergyBalance;
            _context.BalanceHistory.Add(logEntry.ToModel());

            await _context.SaveChangesAsync();
        }

        public async Task PowerDownLifeEnergyAsync(string userId, int count, BalanceHistoryProps logEntry)
        {
            var account = await _context.GetLifeEnergyAccountWithAccessCheckAsync(userId);

            if (account == null)
                return;

            account.PowerDown(count);

            logEntry.EntityId = account.Id;
            logEntry.BalanceType = BalanceType.LifeEnergyBalance;
            _context.BalanceHistory.Add(logEntry.ToModel());
            await _context.SaveChangesAsync();
        }

        public async Task AddExperienceAsync(int childId, IEnumerable<int> characterTraitIds, int count, BalanceHistoryProps logEntry)
        {
            foreach (var characterTraitId in characterTraitIds)
            {
                var trait = await GetOrCreateChildChatacterTraitAsync(childId, characterTraitId);

                trait.AddExperience(count);

                logEntry.EntityId = characterTraitId;
                logEntry.BalanceType = BalanceType.Experience;
                _context.BalanceHistory.Add(logEntry.ToModel());

                await _context.SaveChangesAsync();
            }
        }

        public async Task LoseExperienceAsync(int childId, IEnumerable<int> characterTraitIds, int count, BalanceHistoryProps logEntry)
        {
            foreach (var characterTraitId in characterTraitIds)
            {
                var trait = await GetOrCreateChildChatacterTraitAsync(childId, characterTraitId);

                trait.LoseExperience(count);

                logEntry.EntityId = characterTraitId;
                logEntry.BalanceType = BalanceType.Experience;
                _context.BalanceHistory.Add(logEntry.ToModel());

                await _context.SaveChangesAsync();
            }
        }

        private async Task<ChildCharacterTrait> GetOrCreateChildChatacterTraitAsync(int childId, int characterTraitId)
        {
            var trait = await _context.ChildCharacterTraits.SingleOrDefaultAsync(x => x.CharacterTraitId == characterTraitId);

            if (trait == null)
            {
                trait = new ChildCharacterTrait(childId, characterTraitId);
                _context.ChildCharacterTraits.Add(trait);
            }

            return trait;
        }
    }
}
