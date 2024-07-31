using System;
using System.ComponentModel.DataAnnotations;

namespace ObedientChild.Domain.Habits
{
    public class Habit
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public int Price { get; set; } = 1;

        [Required]
        public string ImageUrl { get; set; }
    }
}
