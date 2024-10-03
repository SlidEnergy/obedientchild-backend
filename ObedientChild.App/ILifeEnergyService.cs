using ObedientChild.Domain;
using ObedientChild.Domain.LifeEnergy;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ObedientChild.App
{
    public interface ILifeEnergyService
    {
        Task PowerUpAsync(string userId, int amount, string title);
        Task PowerDownAsync(string userId, int amount, string title);
        Task<LifeEnergyAccount> GetAccountWithAccessCheckAsync(string userId);
        Task<LifeEnergyAccount> CreateAccountAsync(string userId);
        Task RemoveAccountAsync(string userId);
    }
}