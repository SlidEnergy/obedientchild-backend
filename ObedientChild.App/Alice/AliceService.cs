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

        public async Task<string> HandleAsync(string command, AliceNaturalLanguageUnderstanding nlu)
        {
            if (nlu?.Intents == null)
                return "Я не разобрала запрос, попробуйте снова";

            if (nlu.Intents.violate_rule != null && nlu.Intents.violate_rule.slots != null)
            {
                return await HandleViolateRuleCommand(nlu.Intents.violate_rule.slots);
            }

            if (nlu.Intents.set_habit_status != null && nlu.Intents.set_habit_status.slots != null)
            {
                return await HandleSetHabitStatusCommand(nlu.Intents.set_habit_status.slots);
            }

            if (nlu.Intents.earn_coin != null && nlu.Intents.earn_coin.slots != null)
            {
                return await HandleEarnCoinCommand(nlu.Intents.earn_coin.slots);
            }

            if (nlu.Intents.spend_coin != null && nlu.Intents.spend_coin.slots != null)
            {
                return await HandleSpendCoinCommand(nlu.Intents.spend_coin.slots);
            }

            return "Я не разобрала команду, попробуйте снова";
        }

        private async Task<string> HandleViolateRuleCommand(dynamic slots)
        {
            if (slots == null || slots.command?.value == null)
                return "Я не разобрала запрос, попробуйте снова";

            if (slots.child?.value == null)
                return "Я не определила того, кто нарушил правило";

            if (slots.rule?.value == null)
                return "Я не определила правило";

            var childName = (string)slots.child.value;
            var ruleTitle = (string)slots.rule.value;

            var child = await _context.Children.Where(x => x.Name.ToLower() == childName).SingleOrDefaultAsync();

            if (child == null)
                return "Я не нашла того, кто нарушил правило";

            var rule = await _context.BadDeeds.SingleOrDefaultAsync(x => x.Title.ToLower() == ruleTitle);

            if (rule == null)
                return "Я не нашла правило";

            child.SpendCoin(rule.Price);

            var history = _coinHistoryFactory.CreateSpend(child.Id, rule);

            _context.CoinHistory.Add(history);

            await _context.SaveChangesAsync();

            return "Сделано";
        }

        private async Task<string> HandleEarnCoinCommand(dynamic slots)
        {
            if (slots == null || slots.command?.value == null)
                return "Я не разобрала запрос, попробуйте снова";

            if (slots.child?.value == null)
                return "Я не определила того, кто нарушил правило";

            if (slots.goodDeed?.value == null)
                return "Я не определила за что прибавить монетку";

            var childName = (string)slots.child.value;
            var goodDeedTitle = (string)slots.goodDeed.value;

            var child = await _context.Children.Where(x => x.Name.ToLower() == childName).SingleOrDefaultAsync();

            if (child == null)
                return "Я не нашла того, кто нарушил правило";

            var goodDeed = await _context.GoodDeeds.SingleOrDefaultAsync(x => x.Title.ToLower() == goodDeedTitle);

            if (goodDeed == null)
                return "Я не нашла то, за что прибавить монетку";

            child.EarnCoin(goodDeed.Price);

            var history = _coinHistoryFactory.CreateEarnGoodDeed(child.Id, goodDeed);

            _context.CoinHistory.Add(history);

            await _context.SaveChangesAsync();

            return "Сделано";
        }

        private async Task<string> HandleSpendCoinCommand(dynamic slots)
        {
            if (slots == null || slots.command?.value == null)
                return "Я не разобрала запрос, попробуйте снова";

            if (slots.child?.value == null)
                return "Я не определила того, кто нарушил правило";

            if (slots.reward?.value == null)
                return "Я не определила за что отнять монетку";

            var childName = (string)slots.child.value;
            var rewardTitle = (string)slots.reward.value;

            var child = await _context.Children.Where(x => x.Name.ToLower() == childName).SingleOrDefaultAsync();

            if (child == null)
                return "Я не нашла того, кто нарушил правило";

            var reward = await _context.Rewards.SingleOrDefaultAsync(x => x.Title.ToLower() == rewardTitle);

            if (reward == null)
                return "Я не нашла то, за что отнять монетку";

            child.SpendCoin(reward.Price);

            var history = _coinHistoryFactory.CreateSpend(child.Id, reward);

            _context.CoinHistory.Add(history);

            await _context.SaveChangesAsync();

            return "Сделано";
        }

        private async Task<string> HandleSetHabitStatusCommand(dynamic slots)
        {
            if (slots == null || slots.command?.value == null)
                return "Я не разобрала запрос, попробуйте снова";

            if (slots.child?.value == null)
                return "Я не определила того, кто нарушил правило";
            
            if(slots.habit?.value == null)
                return "Я не определила привычку";

            var childName = (string)slots.child.value;
            var habitTitle = (string)slots.habit.value;
            var command = (string)slots.command.value;
            var result = Enum.TryParse<HabitHistoryStatus>(command, true, out var status);

            if(!result)
                return "Я не определила новый статус для привычки";

            var child = await _context.Children.Where(x => x.Name.ToLower() == childName).SingleOrDefaultAsync();

            if (child == null)
                return "Я не нашла того, кто нарушил правило";

            var habit = await _context.ChildHabits
                .Where(x => x.ChildId == child.Id)
                .Join(_context.Habits, ch => ch.HabitId, h => h.Id, (ch, h) => h)
                .SingleOrDefaultAsync(x => x.Title.ToLower() == habitTitle);

            if (habit == null)
                return "Я не нашла привычку";

            await _habitsService.SetStatusAsync(habit.Id, child.Id, DateOnly.FromDateTime(DateTime.Today), status);

            return "Сделано";
        }
    }
}
