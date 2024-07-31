using System;

namespace ObedientChild.Domain.Habits
{
    public class ChildHabit
    {
        public int ChildId { get; set; }

        public int HabitId { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public ChildHabit()
        {

        }

        public ChildHabit(int childId, int habitId)
        {
            ChildId = childId;
            HabitId = habitId;
            StartDate = DateOnly.FromDateTime(DateTime.Today);
        }
    }
}
