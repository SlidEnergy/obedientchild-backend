using System.Collections.Generic;
using System.Threading.Tasks;
using ObedientChild.Domain;

namespace ObedientChild.App
{
    public interface IGoodDeedService
    {
        Task<List<GoodDeed>> GetListAsync();

        Task<GoodDeed> GetByIdAsync(int id);

        System.Threading.Tasks.Task AddAsync(GoodDeed deed);

        System.Threading.Tasks.Task DeleteAsync(int id);

        Task<GoodDeed> UpdateAsync(GoodDeed deed);
    }
}
