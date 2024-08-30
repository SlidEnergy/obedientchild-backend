using System.Collections.Generic;
using System.Threading.Tasks;
using ObedientChild.Domain;

namespace ObedientChild.App
{
    public interface IRewardsService
    {
        Task<List<Reward>> GetListAsync();

        Task<Reward> GetByIdAsync(int id);

        System.Threading.Tasks.Task AddRewardAsync(Reward reward);

        System.Threading.Tasks.Task DeleteRewardAsync(int id);
        
        Task<Reward> UpdateAsync(Reward reward);
    }
}
