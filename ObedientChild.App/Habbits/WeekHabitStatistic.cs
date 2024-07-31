using System.Collections.Generic;

namespace ObedientChild.App.Habits
{
    public class WeekHabitStatistic
    {
        public int HabitsCount { get; set; }

        public int DoneHabitsCount { get; set; }

        public int SkippedHabitsCount { get; set; }

        public float WeekPercent => HabitsCount == 0 ? 0 : (float)(DoneHabitsCount + SkippedHabitsCount) / (float)HabitsCount;

        public List<DayStatistic> DayStatistics { get; set; } = new List<DayStatistic>();
    }
}
