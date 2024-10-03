using ObedientChild.Domain;
using ObedientChild.Domain.Habits;
using ObedientChild.Domain.Tasks;
using System;

namespace ObedientChild.App.Habits
{
    public class DayTask
    {
        public DateOnly Day { get; set; }

        public int DeedId => _model.Id;

        public string Title => _model.Title;

        public int Price => _model.Price;

        public string ImageUrl => _model.ImageUrl;

        public ChildTaskStatus Status { get; set; }

        private Deed _model;

        public DayTask(DateOnly day, Deed model, ChildTaskStatus status)
        {
            Day = day;
            _model = model;
            Status = status;
        }
    }
}
