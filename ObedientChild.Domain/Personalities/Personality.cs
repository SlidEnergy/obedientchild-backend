using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObedientChild.Domain.Personalities
{
    public class Personality
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        [JsonIgnore]
        public virtual ICollection<DestinyPersonality> DestiniesPersonalities { get; set; }

        [JsonIgnore]
        public virtual ICollection<CharacterTraitPersonality> CharacterTraitsPersonalities { get; set; }

        public Personality() { }

        public Personality(string title)
        {
            Title = title;
        }
    }
}
