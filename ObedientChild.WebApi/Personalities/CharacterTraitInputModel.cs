using ObedientChild.Domain.Personalities;
using System.Collections.Generic;

namespace ObedientChild.WebApi.Personalities
{
    public class CharacterTraitInputModel
    {
        public IEnumerable<int> PersonalityIds { get; set; }
        public CharacterTrait characterTrait {  get; set; }
    }
}
