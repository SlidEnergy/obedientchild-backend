using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObedientChild.App.Balance;
using ObedientChild.Domain;
using ObedientChild.Domain.Habits;

namespace ObedientChild.App.Habits
{
    public class DeedsService : IDeedsService
    {
        private readonly IApplicationDbContext _context;
        private readonly IBalanceHistoryFactory _balanceHistoryFactory;
        private readonly IBalanceService _balanceService;

        public DeedsService(IApplicationDbContext context, IBalanceHistoryFactory balanceHistoryFactory, IBalanceService balanceService)
        {
            _context = context;
            _balanceHistoryFactory = balanceHistoryFactory;
            _balanceService = balanceService;
        }

        public async Task<List<Deed>> GetListAsync(IEnumerable<DeedType> types)
        {
            return await _context.Deeds.Where(x => types.Contains(x.Type)).ToListAsync();
        }
        public async Task<Deed> GetByIdAsync(int id)
        {
            return await _context.Deeds.Include(x => x.CharacterTraitDeeds).SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Deed model)
        {
            _context.Deeds.Add(model);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var model = await _context.Deeds.FindAsync(id);

            if (model != null)
            {
                _context.Deeds.Remove(model);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Deed> UpdateAsync(Deed model, IEnumerable<int> characterTraitIds)
        {
            // Получаем существующую запись Deed из базы данных
            var existingDeed = await _context.Deeds
                .Include(d => d.CharacterTraitDeeds)
                .FirstOrDefaultAsync(d => d.Id == model.Id);

            if (existingDeed == null)
                throw new EntityNotFoundException();

            existingDeed.Title = model.Title;
            existingDeed.Price = model.Price;
            existingDeed.ImageUrl = model.ImageUrl;
            existingDeed.Type = model.Type;

            // Обновляем связь с CharacterTraits вручную
            var currentCharacterTraitDeeds = existingDeed.CharacterTraitDeeds.ToList();

            // Удаляем старые связи, которых нет в новом списке
            foreach (var characterTraitDeed in currentCharacterTraitDeeds)
            {
                if (!characterTraitIds.Contains(characterTraitDeed.CharacterTraitId))
                {
                    existingDeed.CharacterTraitDeeds.Remove(characterTraitDeed);
                }
            }

            // Добавляем новые связи, которых нет в текущем списке
            foreach (var newTraitId in characterTraitIds)
            {
                if (!currentCharacterTraitDeeds.Any(ct => ct.CharacterTraitId == newTraitId))
                {
                    var characterTrait = await _context.CharacterTraits.FindAsync(newTraitId);
                    if (characterTrait != null)
                    {
                        existingDeed.CharacterTraitDeeds.Add(new Domain.Personalities.CharacterTraitDeed(newTraitId, existingDeed.Id));
                    }
                }
            }

            await _context.SaveChangesAsync();
            return existingDeed;
        }

        public async Task<int> InvokeDeedAsync(int childId, int deedId, Deed deed, string userId = null)
        {
            var child = await _context.Children.FindAsync(childId);
            var existingDeed = await _context.Deeds.FindAsync(deedId);

            if (child == null || existingDeed == null)
                throw new EntityNotFoundException();

            if (deed.Type == DeedType.GoodDeed)
            {
                var logEntry = _balanceHistoryFactory.Create(child.Id, deed);
                await _balanceService.EarnCoinAsync(child, deed.Price, logEntry.CloneProps());

                if (existingDeed.CharacterTraitIds != null)
                    await _balanceService.AddExperienceAsync(childId, existingDeed.CharacterTraitIds, existingDeed.Price, logEntry.CloneProps());

                if (userId != null)
                    await _balanceService.PowerDownLifeEnergyAsync(userId, existingDeed.Price, logEntry.CloneProps());
            }
            else if (deed.Type == DeedType.BadDeed)
            {
                var logEntry = _balanceHistoryFactory.Create(child.Id, deed);
                await _balanceService.SpendCoinAsync(child, deed.Price, logEntry.CloneProps());

                if (existingDeed.CharacterTraitIds != null)
                    await _balanceService.LoseExperienceAsync(childId, existingDeed.CharacterTraitIds, existingDeed.Price, logEntry.CloneProps());
            }
            else if (deed.Type == DeedType.Reward)
            {
                var logEntry = _balanceHistoryFactory.Create(child.Id, deed);
                await _balanceService.SpendCoinAsync(child, deed.Price, logEntry.CloneProps());

                if (userId != null)
                    await _balanceService.PowerUpLifeEnergyAsync(userId, existingDeed.Price, logEntry.CloneProps());
            }
            else if (deed.Type == DeedType.Habit)
                throw new ArgumentException("Invoke habbit type doesn't support");

            return child.Balance;
        }
    }
}
