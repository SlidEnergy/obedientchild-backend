using System;

namespace ObedientChild.Domain
{
    public class HabitHistory
    {
        public int Id { get; set; }

        public int ChildId { get; set; }

        public int HabitId { get; set; }

        public HabitHistoryStatus Status { get; set; }

        public DateOnly Day { get; set; }

        public HabitHistory()
        {

        }

        public HabitHistory(DateOnly day, int childId, int habitId, HabitHistoryStatus status)
        {
            Day = day;
            ChildId = childId;
            HabitId = habitId;
            Status = status;
        }
    }
}
