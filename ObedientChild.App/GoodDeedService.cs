using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ObedientChild.Domain;

namespace ObedientChild.App
{
    public class GoodDeedService : IGoodDeedService
	{
		private readonly IApplicationDbContext _context;

		public GoodDeedService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<GoodDeed>> GetListAsync()
		{
			return await _context.GoodDeeds.ToListAsync();
		}

		public async Task<GoodDeed> GetByIdAsync(int id)
		{
			return await _context.GoodDeeds.FindAsync(id);
		}

        public async Task AddAsync(GoodDeed deed)
        {
            _context.GoodDeeds.Add(deed);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var deed = await _context.GoodDeeds.FindAsync(id);

            if (deed != null)
            {
                _context.GoodDeeds.Remove(deed);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<GoodDeed> UpdateAsync(GoodDeed goodDeed)
        {
            _context.Entry(goodDeed).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return goodDeed;
        }
    }
}
