using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObedientChild.Domain.Habits
{
    public class ChildHabit
    {
        public int ChildId { get; set; }

        public virtual Child Child { get; set; }

        public int DeedId { get; set; }

        public virtual Deed Deed { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public ChildHabit()
        {

        }

        public ChildHabit(int childId, int habitId)
        {
            ChildId = childId;
            DeedId = habitId;
            StartDate = DateOnly.FromDateTime(DateTime.Today);
        }
    }
}
