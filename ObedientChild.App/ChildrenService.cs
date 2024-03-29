﻿using System;
using ObedientChild.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ObedientChild.App
{
    public class ChildrenService : IChildrenService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IAuthTokenService _authTokenService;
		private readonly IApplicationDbContext _context;
        private readonly ICoinHistoryFactory _historyFactory;

        public ChildrenService(UserManager<ApplicationUser> userManager, IAuthTokenService authTokenService, IApplicationDbContext context, ICoinHistoryFactory historyFactory)
        {
            _userManager = userManager;
            _authTokenService = authTokenService;
            _context = context;
            _historyFactory = historyFactory;
        }

        public async Task<List<Child>> GetListAsync()
		{
			return await _context.Children.ToListAsync();
		}

		public async Task<Child> GetByIdAsync(int childId)
		{
			return await _context.Children.FindAsync(childId);
		}

        public async Task SaveAvatarAsync(int childId, byte[] data)
        {
            var child = await _context.Children.FindAsync(childId);

            if (child != null)
            {
                child.Avatar = data;

                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> EarnCountAsync(int childId, int count)
        {
            var child = await _context.Children.FindAsync(childId);

            if (child != null)
            {
                child.EarnCoin(count);

                var history = _historyFactory.CreateEarnManual(childId, count);

                _context.CoinHistory.Add(history);

                await _context.SaveChangesAsync();

                return child.Balance;
            }

            return 0;
        }

        public async Task<int> SpendCountAsync(int childId, int count)
        {
            var child = await _context.Children.FindAsync(childId);

            if (child != null)
            {
                child.SpendCoin(count);

                var history = _historyFactory.CreateSpendManual(childId, count);

                _context.CoinHistory.Add(history);

                await _context.SaveChangesAsync();

                return child.Balance;
            }

            return 0;
        }

        public async Task<int> EarnAsync(int childId, Reward reward)
        {
            var child = await _context.Children.FindAsync(childId);

            if (child != null)
            {
                child.EarnCoin(reward.Price);

                var history = _historyFactory.CreateEarn(childId, reward);

                _context.CoinHistory.Add(history);

                await _context.SaveChangesAsync();

                return child.Balance;
            }

            return 0;
        }

        public async Task<int> SpendAsync(int childId, Reward reward)
        {
            var child = await _context.Children.FindAsync(childId);

            if (child != null)
            {
                child.SpendCoin(reward.Price);

                var history = _historyFactory.CreateSpend(childId, reward);

                _context.CoinHistory.Add(history);

                await _context.SaveChangesAsync();

                return child.Balance;
            }

            return 0;
        }

        public async Task SetGoalAsync(int id, int rewardId)
        {
            var child = await _context.Children.FindAsync(id);
            var reward = await _context.Rewards.FindAsync(rewardId);

            if (child != null && reward != null)
            {
                child.SetBigGoal(rewardId);

                await _context.SaveChangesAsync();
            }
        }

        public async Task SetDreamAsync(int id, int rewardId)
        {
            var child = await _context.Children.FindAsync(id);
            var reward = await _context.Rewards.FindAsync(rewardId);

            if (child != null && reward != null)
            {
                child.SetDream(rewardId);

                await _context.SaveChangesAsync();
            }
        }
    }
}
