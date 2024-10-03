using ObedientChild.Domain;
using ObedientChild.Domain.Habits;
using System;

namespace ObedientChild.App;

public interface IBalanceHistoryFactory
{
    BalanceHistory Create(int childId, int count, bool negative = false, string title = null);
    BalanceHistory Create(string userId, int count, bool negative = false, string title = null);
    BalanceHistory Create(int childId, Deed model, bool negative = false);
}