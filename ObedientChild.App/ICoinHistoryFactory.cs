﻿using ObedientChild.Domain;
using ObedientChild.Domain.Habits;
using System;

namespace ObedientChild.App;

public interface ICoinHistoryFactory
{
    CoinHistory CreateEarnManual(int childId, int count);
    CoinHistory CreateEarn(int childId, Reward reward);
    CoinHistory CreateSpendManual(int childId, int count);
    CoinHistory CreateSpend(int childId, Reward reward);

    CoinHistory CreateEarnHabit(int childId, Habit habit);

    CoinHistory CreateSpendHabit(int childId, Habit habit);
}