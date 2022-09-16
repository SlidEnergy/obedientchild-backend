using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ObedientChild.App;
using ObedientChild.Domain;

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
        }

        public DbSet<AuthToken> AuthTokens { get; set; }

        public DbSet<Trustee> Trustee { get; set; }

        public DbSet<ApplicationUser> Users { get; set; }
    }
}
