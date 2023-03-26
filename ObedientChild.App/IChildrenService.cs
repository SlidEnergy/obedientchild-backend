using System.Collections.Generic;
using System.Threading.Tasks;
using ObedientChild.Domain;

namespace ObedientChild.App
{
    public interface IChildrenService
    {
        Task<List<Child>> GetListAsync();

        Task<Child> GetByIdAsync(int childId);
        
        Task SaveAvatarAsync(int childId, byte[] image);

        Task<int> EarnCountAsync(int childId, int count);
        
        Task<int> EarnAsync(int childId, Reward reward);

        Task<int> SpendCountAsync(int childId, int count);

        Task<int> SpendAsync(int childId, Reward reward);

        Task SetGoalAsync(int id, int rewardId);

        Task SetDreamAsync(int id, int rewardId);
    }
}
