using System;

namespace ObedientChild.Domain
{
    public class HabbitHistory
    {
        public int Id { get; set; }

        public int ChildId { get; set; }

        public int HabbitId { get; set; }

        public HabbitHistoryStatus Status { get; set; }

        public DateOnly Day { get; set; }

        public HabbitHistory()
        {

        }

        public HabbitHistory(DateOnly day, int childId, int habbitId, HabbitHistoryStatus status)
        {
            Day = day;
            ChildId = childId;
            HabbitId = habbitId;
            Status = status;
        }
    }
}
