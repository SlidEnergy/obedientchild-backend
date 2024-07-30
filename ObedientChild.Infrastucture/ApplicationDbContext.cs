using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ObedientChild.App;
using ObedientChild.Domain;
using ObedientChild.Domain.Habbits;

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

            modelBuilder.Entity<ChildHabbit>()
                .HasKey(key => new { key.HabbitId, key.ChildId });
        }

        public DbSet<AuthToken> AuthTokens { get; set; }

        public DbSet<Trustee> Trustee { get; set; }
        
        public DbSet<Child> Children { get; set; }

        public DbSet<Reward> Rewards { get; set; }
        public DbSet<GoodDeed> GoodDeeds { get; set; }
        public DbSet<Habbit> Habbits { get; set; }
        public DbSet<BadDeed> BadDeeds { get; set; }

        public DbSet<CoinHistory> CoinHistory { get; set; }
        public DbSet<HabbitHistory> HabbitHistory { get; set; }
        public DbSet<ChildHabbit> ChildHabbits { get; set; }
    }
}
