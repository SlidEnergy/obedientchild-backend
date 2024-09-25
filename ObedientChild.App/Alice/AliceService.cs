using Microsoft.EntityFrameworkCore;
using ObedientChild.App.Habits;
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
        private readonly IHabitsService _habitsService;

        public AliceService(IApplicationDbContext context, ICoinHistoryFactory coinHistoryFactory, IHabitsService habitsService)
        {
            _context = context;
            _coinHistoryFactory = coinHistoryFactory;
            _habitsService = habitsService;
        }

        public async Task<bool> HandleAsync(string command, AliceNaturalLanguageUnderstanding nlu)
        {
            if (nlu?.Intents == null)
                return false;

            if (nlu.Intents.violate_rule != null && nlu.Intents.violate_rule.slots != null)
            {
                return await HandleViolateRuleCommand(nlu.Intents.violate_rule.slots);
            }

            if (nlu.Intents.set_habit_status != null && nlu.Intents.set_habit_status.slots != null)
            {
                return await HandleSetHabitStatusCommand(nlu.Intents.set_habit_status.slots);
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

        private async Task<bool> HandleSetHabitStatusCommand(dynamic slots)
        {
            if (slots == null || slots.command?.value == null || slots.child?.value == null || slots.habit?.value == null)
                return false;

            var childName = (string)slots.child.value;
            var habitTitle = (string)slots.habit.value;
            var command = (string)slots.command.value;
            var result = Enum.TryParse<HabitHistoryStatus>(command, true, out var status);

            if(!result)
                return false;

            var child = await _context.Children.Where(x => x.Name.ToLower() == childName).SingleOrDefaultAsync();

            if (child == null)
                return false;

            var habit = await _context.ChildHabits
                .Where(x => x.ChildId == child.Id)
                .Join(_context.Habits, ch => ch.HabitId, h => h.Id, (ch, h) => h)
                .SingleOrDefaultAsync(x => x.Title.ToLower() == habitTitle);

            if (habit == null)
                return false;

            await _habitsService.SetStatusAsync(habit.Id, child.Id, DateOnly.FromDateTime(DateTime.Today), status);

            return true;
        }
    }
}
