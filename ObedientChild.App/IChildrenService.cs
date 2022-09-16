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
    }
}
