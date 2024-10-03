using ObedientChild.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ObedientChild.App
{
    public interface IBalanceHistoryService
    {
        Task<List<BalanceHistory>> GetListAsync(int childId, BalanceType type);

        Task<BalanceHistory> GetByIdAsync(int id);
        Task RevertAsync(int id);
    }

}
