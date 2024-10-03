using Microsoft.EntityFrameworkCore;
using ObedientChild.App.Balance;
using ObedientChild.Domain;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ObedientChild.App
{
    public interface IBalanceService
    {
        Task AddExperienceAsync(int childId, IEnumerable<int> characterTraitIds, int count, BalanceHistoryProps logEntry);
        Task EarnCoinAsync(Child child, int count, BalanceHistoryProps logEntry);
        Task PowerUpLifeEnergyAsync(string userId, int count, BalanceHistoryProps logEntry);
        Task PowerDownLifeEnergyAsync(string userId, int count, BalanceHistoryProps logEntry);
        Task LoseExperienceAsync(int childId, IEnumerable<int> characterTraitIds, int count, BalanceHistoryProps logEntry);
        Task SpendCoinAsync(Child child, int count, BalanceHistoryProps logEntry);
    }
}