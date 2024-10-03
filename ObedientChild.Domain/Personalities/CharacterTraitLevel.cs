using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObedientChild.Domain.Personalities
{
    public class CharacterTraitLevel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int CharacterTraitId { get; set; }

        public int Level { get; set; }

        public int NeedExperience { get; set; }

        public string ImageUrl { get; set; }

        public CharacterTraitLevel() { }

        public CharacterTraitLevel(string title, int characterTraitId, int level, int needExperience)
        {
            Title = title;
            CharacterTraitId = characterTraitId;
            Level = level;
            NeedExperience = needExperience;
        }
    }
}
