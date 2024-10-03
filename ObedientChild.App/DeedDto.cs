using ObedientChild.Domain.Personalities;
using ObedientChild.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObedientChild.App
{
    public class DeedDto
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public int Price { get; set; } = 1;

        [Required]
        public string ImageUrl { get; set; }

        public IEnumerable<int> CharacterTraitIds { get; set; }

        public DeedType DeedType { get; set; }
    }
}
