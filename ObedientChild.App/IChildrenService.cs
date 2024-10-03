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

        Task SaveAvatarAsync(int childId, byte[] image);

        Task<int> EarnCountAsync(int childId, int count);

        Task<int> SpendCountAsync(int childId, int count);

        Task SetGoalAsync(int id, int rewardId);

        Task SetDreamAsync(int id, int rewardId);

        Task<ChildStatus> AddStatusAsync(int id, ChildStatus childStatus);
        Task DeleteStatusAsync(int id, int childStatusId);
    }
}
