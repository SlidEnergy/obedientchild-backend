using System;

namespace ObedientChild.App.Habbits
{
    public class DayStatistic
    {
        public DateOnly Day { get; set; }

        public int HabbitsCount { get; set; }

        public int DoneHabbitsCount { get; set; }

        public int SkippedHabbitsCount { get; set; }

        public float DayPercent => HabbitsCount == 0 ? 0 : (float)(DoneHabbitsCount + SkippedHabbitsCount) / (float)HabbitsCount;
    }
}
