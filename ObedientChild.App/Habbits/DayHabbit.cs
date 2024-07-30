using ObedientChild.Domain;
using ObedientChild.Domain.Habbits;
using System;

namespace ObedientChild.App.Habbits
{
    public class DayHabbit
    {
        public DateOnly Day { get; set; }

        public int HabbitId => _model.Id;

        public string Title => _model.Title;

        public int Price => _model.Price;

        public string ImageUrl => _model.ImageUrl;

        public HabbitHistoryStatus Status { get; set; }

        private Habbit _model;

        public DayHabbit(DateOnly day, Habbit model, HabbitHistoryStatus status)
        {
            Day = day;
            _model = model;
            Status = status;
        }
    }
}
