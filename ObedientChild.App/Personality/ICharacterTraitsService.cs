using ObedientChild.Domain.Personalities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ObedientChild.App.Personalities
{
    public interface ICharacterTraitsService
    {
        Task<CharacterTrait> AddAsync(IEnumerable<int> personalityIds, CharacterTrait characterTrait);
        Task<CharacterTraitLevel> AddLevelAsync(CharacterTraitLevel characterTraitLevel);
        Task DeleteAsync(int id);
        Task DeleteLevelAsync(int id);
        Task<List<CharacterTraitLevel>> GeLeveltListAsync();
        Task<CharacterTrait> GetByIdAsync(int id);
        Task<ChildCharacterTrait> GetChildCharacterTraitsByIdAsync(int id);
        Task<CharacterTraitLevel> GetLevelByIdAsync(int id);
        Task<List<CharacterTrait>> GetListAsync();
        Task<List<ChildCharacterTrait>> GetListByChildIdAsync(int childId);
        Task<CharacterTrait> UpdateAsync(CharacterTrait characterTrait);
        Task<CharacterTraitLevel> UpdateLevelAsync(CharacterTraitLevel characterTraitLevel);
    }
}