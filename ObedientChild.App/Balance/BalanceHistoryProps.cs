using ObedientChild.Domain;
using System;

namespace ObedientChild.App
{
    public class BalanceHistoryProps
    {
        public string UserId { get; set; }

        public int? EntityId { get; set; }

        public int Amount { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public CoinHistoryType Type { get; set; }

        public DateTime DateTime { get; set; }

        public BalanceType BalanceType { get; set; }
    }
}
