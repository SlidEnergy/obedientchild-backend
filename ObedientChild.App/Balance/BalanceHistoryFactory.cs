using ObedientChild.Domain;
using ObedientChild.Domain.Habits;
using System;

namespace ObedientChild.App
{
    public class BalanceHistoryFactory : IBalanceHistoryFactory
    {
        public BalanceHistory Create(int childId, Deed model, bool negative = false)
        {
            return new BalanceHistory()
            {
                Amount = negative ? -1 * model.Price : model.Price,
                EntityId = childId,
                DateTime = DateTime.UtcNow,
                Title = model.Title,
                Type = ToCoinHistoryType(model.Type),
                ImageUrl = model.ImageUrl
            };
        }

        private CoinHistoryType ToCoinHistoryType(DeedType type)
        {
            switch (type)
            {
                case DeedType.BadDeed:
                    return CoinHistoryType.BadDeed;

                case DeedType.GoodDeed:
                    return CoinHistoryType.GoodDeed;

                case DeedType.Habit:
                    return CoinHistoryType.Habit;

                case DeedType.Reward:
                    return CoinHistoryType.Reward;

                default:
                    throw new ArgumentException($"{type} doesn't support.");
            }
        }

        public BalanceHistory Create(int childId, int count, bool negative, string title= null)
        {
            return new BalanceHistory()
            {
                Amount = negative ? -1 * count : count,
                EntityId = childId,
                DateTime = DateTime.UtcNow,
                Title = title ?? "Manual",
                Type = CoinHistoryType.Manual
            };
        }

        public BalanceHistory Create(string userId, int count, bool negative, string title = null)
        {
            return new BalanceHistory()
            {
                Amount = negative ? -1 * count : count,
                UserId = userId,
                DateTime = DateTime.UtcNow,
                Title = title ?? "Manual",
                Type = CoinHistoryType.Manual
            };
        }
    }
}
