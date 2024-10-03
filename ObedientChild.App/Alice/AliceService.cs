using Microsoft.EntityFrameworkCore;
using ObedientChild.App.Habits;
using ObedientChild.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ObedientChild.App.Alice
{
    public class AliceService : IAliceService
    {
        private readonly IApplicationDbContext _context;
        private readonly IBalanceHistoryFactory _coinHistoryFactory;
        private readonly IHabitsService _habitsService;
        private readonly IBalanceService _balanceService;
        private readonly IChildrenService _childrenService;
        private readonly IDeedsService _deedsService;

        public AliceService(IApplicationDbContext context, IBalanceHistoryFactory coinHistoryFactory, IHabitsService habitsService,
            IBalanceService balanceService, IChildrenService childrenService, IDeedsService deedsService)
        {
            _context = context;
            _coinHistoryFactory = coinHistoryFactory;
            _habitsService = habitsService;
            _balanceService = balanceService;
            _childrenService = childrenService;
            _deedsService = deedsService;
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
                return "Я не определила того, кто совершил действие";

            if (slots.rule?.value == null)
                return "Я не определила правило";

            var childName = (string)slots.child.value;
            var ruleTitle = (string)slots.rule.value;

            var child = await _context.Children.Where(x => x.Name.ToLower() == childName).SingleOrDefaultAsync();

            if (child == null)
                return "Я не нашла того, кто совершил действие";

            var rule = await _context.Deeds.SingleOrDefaultAsync(x => x.Title.ToLower() == ruleTitle && x.Type == DeedType.BadDeed);

            if (rule == null)
                return "Я не нашла правило";

            await _deedsService.InvokeDeedAsync(child.Id, rule.Id, rule);

            return "Сделано";
        }

        private async Task<string> HandleEarnCoinCommand(dynamic slots)
        {
            if (slots == null || slots.command?.value == null)
                return "Я не разобрала запрос, попробуйте снова";

            if (slots.child?.value == null)
                return "Я не определила того, кто совершил действие";

            if (slots.goodDeed?.value == null)
                return "Я не определила за что прибавить монетку";

            var childName = (string)slots.child.value;
            var goodDeedTitle = (string)slots.goodDeed.value;

            var child = await _context.Children.Where(x => x.Name.ToLower() == childName).SingleOrDefaultAsync();

            if (child == null)
                return "Я не нашла того, кто совершил действие";

            var deed = await _context.Deeds.SingleOrDefaultAsync(x => x.Title.ToLower() == goodDeedTitle && x.Type == DeedType.GoodDeed);

            if (deed == null)
                return "Я не нашла то, за что прибавить монетку";

            await _deedsService.InvokeDeedAsync(child.Id, deed.Id, deed);

            return "Сделано";
        }

        private async Task<string> HandleSpendCoinCommand(dynamic slots)
        {
            if (slots == null || slots.command?.value == null)
                return "Я не разобрала запрос, попробуйте снова";

            if (slots.child?.value == null)
                return "Я не определила того, кто совершил действие";

            if (slots.reward?.value == null)
                return "Я не определила за что отнять монетку";

            var childName = (string)slots.child.value;
            var rewardTitle = (string)slots.reward.value;

            var child = await _context.Children.Where(x => x.Name.ToLower() == childName).SingleOrDefaultAsync();

            if (child == null)
                return "Я не нашла того, кто совершил действие";

            var reward = await _context.Deeds.SingleOrDefaultAsync(x => x.Title.ToLower() == rewardTitle && x.Type == DeedType.Reward);

            if (reward == null)
                return "Я не нашла то, за что отнять монетку";

            await _deedsService.InvokeDeedAsync(child.Id, reward.Id, reward);
          
            return "Сделано";
        }

        private async Task<string> HandleSetHabitStatusCommand(dynamic slots)
        {
            if (slots == null || slots.command?.value == null)
                return "Я не разобрала запрос, попробуйте снова";

            if (slots.child?.value == null)
                return "Я не определила того, кто совершил действие";
            
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
                return "Я не нашла того, кто совершил действие";

            var habit = await _context.ChildHabits
                .Where(x => x.ChildId == child.Id)
                .Join(_context.Deeds, ch => ch.DeedId, h => h.Id, (ch, h) => h)
                .SingleOrDefaultAsync(x => x.Title.ToLower() == habitTitle);

            if (habit == null)
                return "Я не нашла привычку";

            await _habitsService.SetStatusAsync(habit.Id, child.Id, DateOnly.FromDateTime(DateTime.Today), status);

            return "Сделано";
        }
    }
}
