using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObedientChild.Domain.Personalities;

namespace ObedientChild.App.Personalities
{
    public class PersonalitiesService : IPersonalitiesService
    {
        private readonly IApplicationDbContext _context;

        public PersonalitiesService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Personality>> GetListAsync()
        {
            return await _context.Personalities.ToListAsync();
        }

        public async Task<Personality> GetByIdAsync(int id)
        {
            return await _context.Personalities.FindAsync(id);
        }

        public async Task<Personality> AddAsync(IEnumerable<int> destinyIds, Personality personality)
        {
            _context.Personalities.Add(personality);
            await _context.SaveChangesAsync();
            foreach (int destinyId in destinyIds)
            {
                _context.DestiniesPersonalities.Add(new DestinyPersonality(destinyId, personality.Id));
            }
            await _context.SaveChangesAsync();

            return personality;
        }

        public async Task DeleteAsync(int id)
        {
            var personality = await _context.Personalities.FindAsync(id);
            if (personality != null)
            {
                _context.Personalities.Remove(personality);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Personality> UpdateAsync(Personality personality)
        {
            _context.Entry(personality).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return personality;
        }

        public async Task<List<Destiny>> GetDestinyListAsync()
        {
            return await _context.Destinies.ToListAsync();
        }

        public async Task<Destiny> GetDestinyByIdAsync(int id)
        {
            return await _context.Destinies.FindAsync(id);
        }

        public async Task<Destiny> AddDestinyAsync(Destiny destiny)
        {
            _context.Destinies.Add(destiny);
            await _context.SaveChangesAsync();

            return destiny;
        }

        public async Task DeleteDestinyAsync(int id)
        {
            var destiny = await _context.Destinies.FindAsync(id);
            if (destiny != null)
            {
                _context.Destinies.Remove(destiny);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Destiny> UpdateDestinyAsync(Destiny destiny)
        {
            _context.Entry(destiny).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return destiny;
        }
    }
}
