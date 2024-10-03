using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ObedientChild.Domain.Personalities
{
    public class ChildCharacterTrait
    {
        public int Id { get; set; }

        public int ChildId { get; set; }

        public int CharacterTraitId { get; set; }

        public virtual CharacterTrait CharacterTrait { get; set; }

        public int Level { get; set; }

        public int Experience { get; set; }

        public ChildCharacterTrait() { }

        public ChildCharacterTrait(int childId, int characterTraitId)
        {
            ChildId = childId;
            CharacterTraitId = characterTraitId;
            Level = 1;
            Experience = 0;
        }

        public int AddExperience(int experience)
        {
            Experience += experience;

            if(Experience < 0)
                Experience = 0;

            return Experience;
        }

        public int LoseExperience(int experience)
        {
            Experience -= experience;

            if (Experience < 0)
                Experience = 0;

            return Experience;
        }
    }
}
