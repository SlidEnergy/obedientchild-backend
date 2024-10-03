using ObedientChild.Domain.Personalities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ObedientChild.App.Personalities
{
    public interface IPersonalitiesService
    {
        Task<Personality> AddAsync(IEnumerable<int> destinyIds, Personality personality);
        Task<Destiny> AddDestinyAsync(Destiny destiny);
        Task DeleteAsync(int id);
        Task DeleteDestinyAsync(int id);
        Task<Personality> GetByIdAsync(int id);
        Task<Destiny> GetDestinyByIdAsync(int id);
        Task<List<Destiny>> GetDestinyListAsync();
        Task<List<Personality>> GetListAsync();
        Task<Personality> UpdateAsync(Personality personality);
        Task<Destiny> UpdateDestinyAsync(Destiny destiny);
    }
}