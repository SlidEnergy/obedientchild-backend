using ObedientChild.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ObedientChild.App
{
    public interface ICoinHistoryService
    {
        Task<List<CoinHistory>> GetListAsync();

        Task<CoinHistory> GetByIdAsync(int id);
        Task AddAsync(CoinHistory deed);
        Task RevertAsync(int id);
    }

}
