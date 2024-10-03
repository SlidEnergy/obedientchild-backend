using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObedientChild.Domain.Personalities
{
    public class Destiny
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public virtual ICollection<DestinyPersonality> DestiniesPersonalities { get; set; }

        public Destiny() { }

        public Destiny(string title)
        {
            Title = title;
        }
    }
}
