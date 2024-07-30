using System;

namespace ObedientChild.Domain.Habbits
{
    public class ChildHabbit
    {
        public int ChildId { get; set; }

        public int HabbitId { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public ChildHabbit()
        {

        }

        public ChildHabbit(int childId, int habbitId)
        {
            ChildId = childId;
            HabbitId = habbitId;
            StartDate = DateOnly.FromDateTime(DateTime.Today);
        }
    }
}
