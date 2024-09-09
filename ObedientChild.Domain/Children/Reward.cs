using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ObedientChild.Domain
{
    public class Reward
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public int Price { get; set; } = 1;

        [Required]
        public string ImageUrl { get; set; }
    }
}
