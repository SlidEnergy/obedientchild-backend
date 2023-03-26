using System.Collections.Generic;
using System.Threading.Tasks;
using ObedientChild.Domain;

namespace ObedientChild.App
{
    public interface IBadDeedService
    {
        Task<List<BadDeed>> GetListAsync();

        Task<BadDeed> GetByIdAsync(int id);

        Task AddAsync(BadDeed deed);

        Task DeleteAsync(int id);

        Task<BadDeed> UpdateAsync(BadDeed deed);
    }
}
