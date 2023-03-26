using System.Collections.Generic;
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

        public async Task<List<CoinHistory>> GetListAsync()
        {
            return await _context.CoinHistory.ToListAsync();
        }

        public async Task<CoinHistory> GetByIdAsync(int id)
        {
            return await _context.CoinHistory.FindAsync(id);
        }

        public async Task AddAsync(CoinHistory deed)
        {
            _context.CoinHistory.Add(deed);

            await _context.SaveChangesAsync();
        }

        public async Task RevertAsync(int id)
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
