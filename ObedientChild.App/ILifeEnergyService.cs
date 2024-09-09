using ObedientChild.Domain;
using ObedientChild.Domain.LifeEnergy;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ObedientChild.App
{
    public interface ILifeEnergyService
    {
        Task<LifeEnergyHistory> PowerUpAsync(string userId, int amount, string title);
        Task<LifeEnergyHistory> PowerDownAsync(string userId, int amount, string title);
        Task<LifeEnergyAccount> GetAccountWithAccessCheckAsync(string userId);
        Task<LifeEnergyHistory> GetHistoryByIdAsync(string userId, int id);
        Task<List<LifeEnergyHistory>> GetHistoryListWithAccessCheckAsync(string userId);
        Task RevertHistoryAsync(string userId, int id);
        Task<LifeEnergyAccount> CreateAccountAsync(string userId);
        Task RemoveAccountAsync(string userId);
    }
}