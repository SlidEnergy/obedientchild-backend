using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ObedientChild.Domain;

namespace ObedientChild.App
{
    public interface IChildrenService
    {
        Task<List<Child>> GetListAsync();

        Task<ChildView> GetByIdAsync(int childId);

        System.Threading.Tasks.Task SaveAvatarAsync(int childId, byte[] image);

        Task<int> EarnCountAsync(int childId, int count);
        
        Task<int> EarnAsync(int childId, Reward reward);

        Task<int> SpendCountAsync(int childId, int count);

        Task<int> SpendAsync(int childId, Reward reward);

        System.Threading.Tasks.Task SetGoalAsync(int id, int rewardId);

        System.Threading.Tasks.Task SetDreamAsync(int id, int rewardId);

        System.Threading.Tasks.Task AddStatusAsync(int id, ChildStatus childStatus);
        System.Threading.Tasks.Task DeleteStatusAsync(int id, int childStatusId);
    }
}
