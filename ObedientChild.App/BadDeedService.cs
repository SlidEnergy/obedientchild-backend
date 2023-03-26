using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ObedientChild.Domain;

namespace ObedientChild.App
{
    public class BadDeedService : IBadDeedService
	{
		private readonly IApplicationDbContext _context;

		public BadDeedService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<BadDeed>> GetListAsync()
		{
			return await _context.BadDeeds.ToListAsync();
		}

		public async Task<BadDeed> GetByIdAsync(int id)
		{
			return await _context.BadDeeds.FindAsync(id);
		}

        public async Task AddAsync(BadDeed deed)
        {
            _context.BadDeeds.Add(deed);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var deed = await _context.BadDeeds.FindAsync(id);

            if (deed != null)
            {
                _context.BadDeeds.Remove(deed);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<BadDeed> UpdateAsync(BadDeed badDeed)
        {
            _context.Entry(badDeed).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return badDeed;
        }
    }
}
