using Microsoft.EntityFrameworkCore;
using ObedientChild.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ObedientChild.App.Alice
{
    public class AliceService : IAliceService
    {
        private readonly IApplicationDbContext _context;
        private readonly ICoinHistoryFactory _coinHistoryFactory;

        public AliceService(IApplicationDbContext context, ICoinHistoryFactory coinHistoryFactory)
        {
            _context = context;
            _coinHistoryFactory = coinHistoryFactory;
        }

        public async Task<bool> HandleAsync(string command, AliceNaturalLanguageUnderstanding nlu)
        {
            if (nlu?.Intents == null)
                return false;

            if (nlu.Intents.violate_rule != null && nlu.Intents.violate_rule.slots != null)
            {
                return await HandleViolateRuleCommand(nlu.Intents.violate_rule.slots);
            }

            if (nlu.Intents.done_habbit != null && nlu.Intents.done_habbit.slots != null)
            {
                return await HandleDoneHabitCommand(nlu.Intents.done_habbit.slots);
            }

            return false;
        }

        private async Task<bool> HandleViolateRuleCommand(dynamic slots)
        {
            if (slots == null || slots.command?.value == null || slots.child?.value == null || slots.rule?.value == null)
                return false;

            var childName = (string)slots.child.value;
            var ruleTitle = (string)slots.rule.value;

            var child = await _context.Children.Where(x => x.Name.ToLower() == childName).SingleOrDefaultAsync();

            if (child == null)
                return false;

            var rule = await _context.BadDeeds.SingleOrDefaultAsync(x => x.Title.ToLower() == ruleTitle);

            if (rule == null)
                return false;

            child.SpendCoin(rule.Price);

            var history = _coinHistoryFactory.CreateSpend(child.Id, rule);

            _context.CoinHistory.Add(history);

            await _context.SaveChangesAsync();

            return true;
        }

        private async Task<bool> HandleDoneHabitCommand(dynamic slots)
        {
            if (slots == null || slots.command?.value == null || slots.child?.value == null || slots.habit?.value == null)
                return false;

            var childName = (string)slots.child.value;
            var habitTitle = (string)slots.habit.value;

            var child = await _context.Children.Where(x => x.Name.ToLower() == childName).SingleOrDefaultAsync();

            if (child == null)
                return false;

            var habit = await _context.Habits.SingleOrDefaultAsync(x => x.Title.ToLower() == habitTitle);

            if (habit == null)
                return false;

            child.SpendCoin(habit.Price);

            var history = _coinHistoryFactory.CreateSpendHabit(child.Id, habit);

            _context.CoinHistory.Add(history);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
