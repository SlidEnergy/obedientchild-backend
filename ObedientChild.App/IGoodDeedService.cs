using System.Collections.Generic;
using System.Threading.Tasks;
using ObedientChild.Domain;

namespace ObedientChild.App
{
    public interface IGoodDeedService
    {
        Task<List<GoodDeed>> GetListAsync();

        Task<GoodDeed> GetByIdAsync(int id);

        Task AddAsync(GoodDeed deed);

        Task DeleteAsync(int id);
    }
}
