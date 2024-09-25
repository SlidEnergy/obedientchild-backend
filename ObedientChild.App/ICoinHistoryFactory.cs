using ObedientChild.Domain;
using ObedientChild.Domain.Habits;
using System;

namespace ObedientChild.App;

public interface ICoinHistoryFactory
{
    CoinHistory CreateEarnManual(int childId, int count);
    CoinHistory CreateEarn(int childId, Reward reward);
    CoinHistory CreateEarn(int childId, BadDeed badDeed);
    CoinHistory CreateSpendManual(int childId, int count);
    CoinHistory CreateSpend(int childId, Reward reward);

    CoinHistory CreateSpend(int childId, BadDeed badDeed);

    CoinHistory CreateEarnHabit(int childId, Habit habit);

    CoinHistory CreateSpendHabit(int childId, Habit habit);

    CoinHistory CreateEarnGoodDeed(int childId, GoodDeed goodDeed);
    CoinHistory CreateSpendGoodDeed(int childId, GoodDeed goodDeed);
}