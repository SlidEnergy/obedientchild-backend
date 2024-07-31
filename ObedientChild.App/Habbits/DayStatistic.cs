using System;

namespace ObedientChild.App.Habits
{
    public class DayStatistic
    {
        public DateOnly Day { get; set; }

        public int HabitsCount { get; set; }

        public int DoneHabitsCount { get; set; }

        public int SkippedHabitsCount { get; set; }

        public int FailedHabitsCount { get; set; }

        public float DayPercent => HabitsCount == 0 ? 0 : (float)(DoneHabitsCount + SkippedHabitsCount) / (float)HabitsCount;
    }
}
