using ObedientChild.Domain;
using ObedientChild.Domain.Habbits;
using System;

namespace ObedientChild.App;

public interface ICoinHistoryFactory
{
    CoinHistory CreateEarnManual(int childId, int count);
    CoinHistory CreateEarn(int childId, Reward reward);
    CoinHistory CreateSpendManual(int childId, int count);
    CoinHistory CreateSpend(int childId, Reward reward);

    CoinHistory CreateEarnHabbit(int childId, Habbit habbit);

    CoinHistory CreateSpendHabbit(int childId, Habbit habbit);
}