using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObedientChild.Domain.Personalities
{
    public class DestinyPersonality
    {
        public int DestinyId { get; set; }

        [Required]
        public virtual Destiny Destiny { get; set; }

        public int PersonalityId { get; set; }

        [Required]
        public virtual Personality Personality { get; set; }

        public DestinyPersonality() { }

        public DestinyPersonality(int destinyId, int personalityId)
        {
            DestinyId = destinyId;
            PersonalityId = personalityId;
        }
    }
}
