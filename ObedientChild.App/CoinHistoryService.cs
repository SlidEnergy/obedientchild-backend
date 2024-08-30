using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ObedientChild.Domain;

namespace ObedientChild.App
{
    public class CoinHistoryService : ICoinHistoryService
    {
        private readonly IApplicationDbContext _context;

        public CoinHistoryService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CoinHistory>> GetListAsync(int childId)
        {
            if(childId == 0)
                return await _context.CoinHistory.ToListAsync();

            // TODO: add index by childId

            return await _context.CoinHistory.Where(x => x.ChildId == childId).ToListAsync();
        }

        public async Task<CoinHistory> GetByIdAsync(int id)
        {
            return await _context.CoinHistory.FindAsync(id);
        }

        public async System.Threading.Tasks.Task RevertAsync(int id)
        {
            var history = await _context.CoinHistory.FindAsync(id);

            if (history == null)
                return;
         
            var child = await _context.Children.FindAsync(history.ChildId);

            if (child == null)
                return;

            child.Balance -= history.Amount;

            _context.CoinHistory.Remove(history);
            await _context.SaveChangesAsync();
        }
    }
}
