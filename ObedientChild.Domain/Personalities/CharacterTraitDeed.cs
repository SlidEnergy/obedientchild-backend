using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObedientChild.Domain.Personalities
{
    public class CharacterTraitDeed
    {
        public int CharacterTraitId { get; set; }

        [Required]
        public virtual CharacterTrait CharacterTrait { get; set; }

        public int DeedId { get; set; }

        [Required]
        public virtual Deed Deed { get; set; }

        public CharacterTraitDeed() { }

        public CharacterTraitDeed(int characterTraitId, int deedId)
        {
            CharacterTraitId = characterTraitId;
            DeedId = deedId;
        }
    }
}
