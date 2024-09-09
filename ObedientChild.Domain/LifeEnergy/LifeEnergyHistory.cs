using System;

namespace ObedientChild.Domain
{
    public class LifeEnergyHistory
    {
        public int Id { get; set; }
        public int LifeEnergyAccountId { get; set; }

        public int Amount { get; set; }

        public string Title { get; set; }

        public DateTime DateTime { get; set; }

        public LifeEnergyHistory(int amount, string title, int lifeEnergyAccountId)
        {
            DateTime = DateTime.UtcNow;
            Amount = amount;
            Title = title;
            LifeEnergyAccountId = lifeEnergyAccountId;
        }
    }
}
