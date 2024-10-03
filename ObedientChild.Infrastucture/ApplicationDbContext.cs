using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ObedientChild.App;
using ObedientChild.Domain;
using ObedientChild.Domain.Habits;
using ObedientChild.Domain.LifeEnergy;
using ObedientChild.Domain.Personalities;

namespace ObedientChild.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ChildHabit>()
                .HasKey(key => new { key.DeedId, key.ChildId });

            modelBuilder.Entity<TrusteeLifeEnergyAccount>()
                .HasKey(key => new { key.TrusteeId, key.LifeEnergyAccountId });

            modelBuilder.Entity<CharacterTraitPersonality>()
                .HasKey(key => new { key.PersonalityId, key.CharacterTraitId });

            modelBuilder.Entity<CharacterTraitPersonality>()
                .HasOne(ctd => ctd.Personality)
                .WithMany(d => d.CharacterTraitsPersonalities)
                .HasForeignKey(ctd => ctd.PersonalityId);

            modelBuilder.Entity<CharacterTraitPersonality>()
                .HasOne(ctd => ctd.CharacterTrait)
                .WithMany(d => d.CharacterTraitsPersonalities)
                .HasForeignKey(ctd => ctd.CharacterTraitId);

            modelBuilder.Entity<DestinyPersonality>()
                .HasKey(key => new { key.DestinyId, key.PersonalityId });

            modelBuilder.Entity<DestinyPersonality>()
                .HasOne(ctd => ctd.Destiny)
                .WithMany(d => d.DestiniesPersonalities)
                .HasForeignKey(ctd => ctd.DestinyId);

            modelBuilder.Entity<DestinyPersonality>()
                .HasOne(ctd => ctd.Personality)
                .WithMany(d => d.DestiniesPersonalities)
                .HasForeignKey(ctd => ctd.PersonalityId);

            modelBuilder.Entity<CharacterTraitDeed>()
                .HasKey(key => new { key.CharacterTraitId, key.DeedId });

            modelBuilder.Entity<CharacterTraitDeed>()
                .HasOne(ctd => ctd.CharacterTrait)
                .WithMany(ct => ct.CharacterTraitDeeds)
                .HasForeignKey(ctd => ctd.CharacterTraitId);

            modelBuilder.Entity<CharacterTraitDeed>()
                .HasOne(ctd => ctd.Deed)
                .WithMany(d => d.CharacterTraitDeeds)
                .HasForeignKey(ctd => ctd.DeedId);
        }

        public DbSet<AuthToken> AuthTokens { get; set; }

        public DbSet<Trustee> Trustee { get; set; }
        
        public DbSet<Child> Children { get; set; }

        public DbSet<Deed> Deeds { get; set; }

        public DbSet<BalanceHistory> BalanceHistory { get; set; }
        public DbSet<HabitHistory> HabitHistory { get; set; }
        public DbSet<ChildHabit> ChildHabits { get; set; }

        public DbSet<ChildStatus> ChildStatuses { get; set; }

        public DbSet<ChildTask> ChildTasks { get; set; }

        public DbSet<TrusteeLifeEnergyAccount> TrusteeLifeEnergyAccounts { get; set; }

        public DbSet<LifeEnergyAccount> LifeEnergyAccounts { get; set;}

        public DbSet<Destiny> Destinies{ get; set; }

        public DbSet<Personality> Personalities { get; set; }

        public DbSet<CharacterTrait> CharacterTraits { get; set; }

        public DbSet<CharacterTraitLevel> CharacterTraitsLevel { get; set; }
        public DbSet<CharacterTraitDeed> CharacterTraitsDeeds { get; set; }

        public DbSet<ChildCharacterTrait> ChildCharacterTraits { get; set;}

        public DbSet<CharacterTraitPersonality> PersonalitiesCharacterTraits { get; set; }
        public DbSet<DestinyPersonality> DestiniesPersonalities{ get; set; }
    }
}
