using Domain.Entities;
using Domain.Entities.Identity;
using Infrastructure.Data.Persistence.Configuration;
using Infrastructure.Data.Persistence.Configuration.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Persistence
{
    public class CactusPassDbContext : IdentityDbContext<AppUser, AppRole, string, AppUserClaim, AppUserRole,
        AppUserLogin, AppRoleClaim, AppUserToken>
    {
        public virtual DbSet<UserJwtToken> UserJwtTokens { get; set; }

        public virtual DbSet<Password> Passwords { get; set; }

        public virtual DbSet<Note> Notes { get; set; }

        public CactusPassDbContext(DbContextOptions<CactusPassDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder?.ApplyConfiguration(new UserJwtTokenConfiguration());

            modelBuilder?.ApplyConfiguration(new AppUserConfiguration());

            modelBuilder?.ApplyConfiguration(new PasswordConfiguration());

            modelBuilder?.ApplyConfiguration(new NoteConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
