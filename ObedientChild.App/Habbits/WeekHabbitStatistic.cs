using System.Collections.Generic;

namespace ObedientChild.App.Habbits
{
    public class WeekHabbitStatistic
    {
        public int HabbitsCount { get; set; }

        public int DoneHabbitsCount { get; set; }

        public int SkippedHabbitsCount { get; set; }

        public float WeekPercent => HabbitsCount == 0 ? 0 : (float)(DoneHabbitsCount + SkippedHabbitsCount) / (float)HabbitsCount;

        public List<DayStatistic> DayStatistics { get; set; } = new List<DayStatistic>();
    }
}
