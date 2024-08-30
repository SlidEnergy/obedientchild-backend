using System.Collections.Generic;
using System.Threading.Tasks;
using ObedientChild.Domain;

namespace ObedientChild.App
{
    public interface IBadDeedService
    {
        Task<List<BadDeed>> GetListAsync();

        Task<BadDeed> GetByIdAsync(int id);

        System.Threading.Tasks.Task AddAsync(BadDeed deed);

        System.Threading.Tasks.Task DeleteAsync(int id);

        Task<BadDeed> UpdateAsync(BadDeed deed);
    }
}
