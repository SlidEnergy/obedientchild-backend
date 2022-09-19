using System.Collections.Generic;
using System.Threading.Tasks;
using ObedientChild.Domain;

namespace ObedientChild.App
{
    public interface IRewardsService
    {
        Task<List<Reward>> GetListAsync();

        Task<Reward> GetByIdAsync(int id);

        Task AddRewardAsync(Reward reward);

        Task DeleteRewardAsync(int id);
    }
}
