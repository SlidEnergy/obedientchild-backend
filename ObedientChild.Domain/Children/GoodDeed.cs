using System.ComponentModel.DataAnnotations;

namespace ObedientChild.Domain
{
    public class GoodDeed
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public int Price { get; set; } = 1;

        [Required]
        public string ImageUrl { get; set; }
    }
}
