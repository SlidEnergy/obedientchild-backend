using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ObedientChild.App;
using ObedientChild.Domain;
using ObedientChild.Domain.Habits;

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
                .HasKey(key => new { key.HabitId, key.ChildId });
        }

        public DbSet<AuthToken> AuthTokens { get; set; }

        public DbSet<Trustee> Trustee { get; set; }
        
        public DbSet<Child> Children { get; set; }

        public DbSet<Reward> Rewards { get; set; }
        public DbSet<GoodDeed> GoodDeeds { get; set; }
        public DbSet<Habit> Habits { get; set; }
        public DbSet<BadDeed> BadDeeds { get; set; }

        public DbSet<CoinHistory> CoinHistory { get; set; }
        public DbSet<HabitHistory> HabitHistory { get; set; }
        public DbSet<ChildHabit> ChildHabits { get; set; }

        public DbSet<ChildStatus> ChildStatuses { get; set; }

        public DbSet<ChildTask> ChildTasks { get; set; }
    }
}
