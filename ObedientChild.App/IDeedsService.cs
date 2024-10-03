using ObedientChild.Domain;
using ObedientChild.Domain.Habits;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ObedientChild.App.Habits
{
    public interface IDeedsService
    {
        Task AddAsync(Deed model);
        Task DeleteAsync(int id);
        Task<Deed> GetByIdAsync(int id);
        Task<List<Deed>> GetListAsync(DeedType type);
        Task<Deed> UpdateAsync(Deed model, IEnumerable<int> characterTraitIds);
        Task<int> InvokeDeedAsync(int childId, int id, Deed deed, string userId = null);
    }
}