using ObedientChild.Domain.Personalities;
using System.Collections.Generic;

namespace ObedientChild.WebApi.Personalities
{
    public class CreatePersonalityInputModel
    {
        public IEnumerable<int> DestinyIds { get; set; }

        public Personality Personality { get; set; }
    }
}
