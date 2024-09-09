using System;

namespace ObedientChild.Domain
{
    public class CoinHistory
    {
        public int Id { get; set; }

        public int ChildId { get; set; }

        public int Amount { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public CoinHistoryType Type { get; set; }

        public DateTime DateTime { get; set; }
    }
}
