﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ObedientChild.Domain;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ObedientChild.Domain.Habits;

namespace ObedientChild.App
{
    public interface IApplicationDbContext
	{
		DbSet<Trustee> Trustee { get; set; }
		DbSet<AuthToken> AuthTokens { get; set; }
		DbSet<ApplicationUser> Users { get; set; }

		DbSet<IdentityRole> Roles { get; set; }

		DbSet<IdentityUserRole<string>> UserRoles { get; set; }

		DbSet<Child> Children { get; set; }

        DbSet<Reward> Rewards { get; set; }

        DbSet<GoodDeed> GoodDeeds { get; set; }

        DbSet<Habit> Habits { get; set; }

        DbSet<BadDeed> BadDeeds { get; set; }

		DbSet<CoinHistory> CoinHistory { get; set; }
		DbSet<HabitHistory> HabitHistory { get; set; }
		DbSet<ChildHabit> ChildHabits { get; set; }

        DbSet<ChildStatus> ChildStatuses { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

		EntityEntry Entry(object entity);

		EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
	}
}