using ObedientChild.Domain;
using ObedientChild.Domain.Habits;
using ObedientChild.Domain.Tasks;
using System;

namespace ObedientChild.App.Habits
{
    public class DayTask
    {
        public DateOnly Day { get; set; }

        public int GoodDeedId => _model.Id;

        public string Title => _model.Title;

        public int Price => _model.Price;

        public string ImageUrl => _model.ImageUrl;

        public ChildTaskStatus Status { get; set; }

        private GoodDeed _model;

        public DayTask(DateOnly day, GoodDeed model, ChildTaskStatus status)
        {
            Day = day;
            _model = model;
            Status = status;
        }
    }
}
