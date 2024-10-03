using Newtonsoft.Json;
using ObedientChild.Domain.Personalities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ObedientChild.Domain
{
    public class Deed
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public int Price { get; set; } = 1;

        [Required]
        public string ImageUrl { get; set; }

        public IEnumerable<int> CharacterTraitIds => CharacterTraitDeeds?.Select(x => x.CharacterTraitId);

        [JsonIgnore]
        public virtual ICollection<CharacterTraitDeed> CharacterTraitDeeds { get; set; }

        public DeedType Type { get; set; }
    }
}
