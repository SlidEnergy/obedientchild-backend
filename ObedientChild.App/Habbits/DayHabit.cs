using ObedientChild.Domain;
using ObedientChild.Domain.Habits;
using System;

namespace ObedientChild.App.Habits
{
    public class DayHabit
    {
        public DateOnly Day { get; set; }

        public int HabitId => _model.Id;

        public string Title => _model.Title;

        public int Price => _model.Price;

        public string ImageUrl => _model.ImageUrl;

        public HabitHistoryStatus Status { get; set; }

        private Deed _model;

        public DayHabit(DateOnly day, Deed model, HabitHistoryStatus status)
        {
            Day = day;
            _model = model;
            Status = status;
        }
    }
}
