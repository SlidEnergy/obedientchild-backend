using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObedientChild.Domain.Personalities
{
    public class CharacterTrait
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public virtual ICollection<CharacterTraitLevel> Levels { get; set; }

        [JsonIgnore]
        public virtual ICollection<CharacterTraitDeed> CharacterTraitDeeds { get; set; }

        [JsonIgnore]
        public virtual ICollection<CharacterTraitPersonality> CharacterTraitsPersonalities { get; set; }

        public CharacterTrait() { }

        public CharacterTrait(string title)
        {
            Title = title;
        }
    }
}
