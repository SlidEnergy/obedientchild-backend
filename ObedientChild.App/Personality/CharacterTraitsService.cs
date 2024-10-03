using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ObedientChild.Domain.Personalities;

namespace ObedientChild.App.Personalities
{
    public class CharacterTraitsService : ICharacterTraitsService
    {
        private readonly IApplicationDbContext _context;

        public CharacterTraitsService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CharacterTrait>> GetListAsync()
        {
            return await _context.CharacterTraits.Include(ct => ct.Levels).ToListAsync();
        }

        public async Task<CharacterTrait> GetByIdAsync(int id)
        {
            return await _context.CharacterTraits.Include(ct => ct.Levels).FirstOrDefaultAsync(ct => ct.Id == id);
        }

        public async Task<CharacterTrait> AddAsync(IEnumerable<int> personalityIds, CharacterTrait characterTrait)
        {
            _context.CharacterTraits.Add(characterTrait);
            await _context.SaveChangesAsync();
            foreach (int personalityId in personalityIds)
            {
                _context.PersonalitiesCharacterTraits.Add(new CharacterTraitPersonality(personalityId, characterTrait.Id));
            }
            await _context.SaveChangesAsync();

            return characterTrait;
        }

        public async Task DeleteAsync(int id)
        {
            var characterTrait = await _context.CharacterTraits.FindAsync(id);
            if (characterTrait != null)
            {
                _context.CharacterTraits.Remove(characterTrait);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<CharacterTrait> UpdateAsync(CharacterTrait characterTrait)
        {
            _context.Entry(characterTrait).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return characterTrait;
        }

        public async Task<List<CharacterTraitLevel>> GeLeveltListAsync()
        {
            return await _context.CharacterTraitsLevel.ToListAsync();
        }

        public async Task<CharacterTraitLevel> GetLevelByIdAsync(int id)
        {
            return await _context.CharacterTraitsLevel.FindAsync(id);
        }

        public async Task<CharacterTraitLevel> AddLevelAsync(CharacterTraitLevel characterTraitLevel)
        {
            _context.CharacterTraitsLevel.Add(characterTraitLevel);
            await _context.SaveChangesAsync();

            return characterTraitLevel;
        }

        public async Task DeleteLevelAsync(int id)
        {
            var characterTraitLevel = await _context.CharacterTraitsLevel.FindAsync(id);
            if (characterTraitLevel != null)
            {
                _context.CharacterTraitsLevel.Remove(characterTraitLevel);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<CharacterTraitLevel> UpdateLevelAsync(CharacterTraitLevel characterTraitLevel)
        {
            _context.Entry(characterTraitLevel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return characterTraitLevel;
        }

        public async Task<List<ChildCharacterTrait>> GetListByChildIdAsync(int childId)
        {
            return await _context.ChildCharacterTraits
                .Include(cct => cct.CharacterTrait)
                .Include(cct => cct.CharacterTrait.Levels)
                .Where(cct => cct.ChildId == childId)
                .ToListAsync();
        }

        public async Task<ChildCharacterTrait> GetChildCharacterTraitsByIdAsync(int id)
        {
            return await _context.ChildCharacterTraits
                .Include(cct => cct.CharacterTrait)
                .FirstOrDefaultAsync(cct => cct.Id == id);
        }
    }
}
