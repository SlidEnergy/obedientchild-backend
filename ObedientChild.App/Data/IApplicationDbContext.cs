using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ObedientChild.Domain;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ObedientChild.Domain.Habits;
using ObedientChild.Domain.LifeEnergy;
using ObedientChild.Domain.Personalities;

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

        DbSet<Deed> Deeds { get; set; }

		DbSet<BalanceHistory> BalanceHistory { get; set; }
		DbSet<HabitHistory> HabitHistory { get; set; }
		DbSet<ChildHabit> ChildHabits { get; set; }

        DbSet<ChildStatus> ChildStatuses { get; set; }

        DbSet<ChildTask> ChildTasks { get; set; }

        DbSet<TrusteeLifeEnergyAccount> TrusteeLifeEnergyAccounts { get; set; }

        DbSet<LifeEnergyAccount> LifeEnergyAccounts { get; set; }

        DbSet<Destiny> Destinies{ get; set; }
        DbSet<Personality> Personalities { get; set; }

        DbSet<CharacterTrait> CharacterTraits { get; set; }

        DbSet<CharacterTraitLevel> CharacterTraitsLevel { get; set; }

        DbSet<ChildCharacterTrait> ChildCharacterTraits { get; set; }

        DbSet<CharacterTraitPersonality> PersonalitiesCharacterTraits { get; set; }

        DbSet<DestinyPersonality> DestiniesPersonalities { get; set; }
        DbSet<CharacterTraitDeed> CharacterTraitsDeeds { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

		EntityEntry Entry(object entity);

		EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
	}
}