using ObedientChild.Domain;
using ObedientChild.Domain.Habbits;
using System;

namespace ObedientChild.App
{
    public class CoinHistoryFactory : ICoinHistoryFactory
    {
        public CoinHistory CreateEarnManual(int childId, int count)
        {
            return new CoinHistory()
            {
                Amount = count,
                ChildId = childId,
                DateTime = DateTime.UtcNow,
                Title = "Manual",
                Type = HistoryType.Manual
            };
        }

        public CoinHistory CreateEarn(int childId, Reward reward)
        {
            return new CoinHistory()
            {
                Amount = reward.Price,
                ChildId = childId,
                DateTime = DateTime.UtcNow,
                Title = reward.Title,
                Type = HistoryType.GoodDeed,
                ImageUrl = reward.ImageUrl
            };
        }

        public CoinHistory CreateSpendManual(int childId, int count)
        {
            return new CoinHistory()
            {
                Amount = -count,
                ChildId = childId,
                DateTime = DateTime.UtcNow,
                Title = "Manual",
                Type = HistoryType.Manual
            };
        }

        public CoinHistory CreateSpend(int childId, Reward reward)
        {
            return new CoinHistory()
            {
                Amount = -reward.Price,
                ChildId = childId,
                DateTime = DateTime.UtcNow,
                Title = reward.Title,
                Type = HistoryType.BadDeed, 
                ImageUrl = reward.ImageUrl
            };
        }

        public CoinHistory CreateEarnHabbit(int childId, Habbit habbit)
        {
            return new CoinHistory()
            {
                Amount = habbit.Price,
                ChildId = childId,
                DateTime = DateTime.UtcNow,
                Title = habbit.Title,
                Type = HistoryType.Habbit,
                ImageUrl = habbit.ImageUrl
            };
        }

        public CoinHistory CreateSpendHabbit(int childId, Habbit habbit)
        {
            return new CoinHistory()
            {
                Amount = -habbit.Price,
                ChildId = childId,
                DateTime = DateTime.UtcNow,
                Title = habbit.Title,
                Type = HistoryType.Habbit,
                ImageUrl = habbit.ImageUrl
            };
        }
    }
}
