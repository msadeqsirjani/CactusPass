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

        public virtual DbSet<Note> Messages { get; set; }

        public CactusPassDbContext(DbContextOptions<CactusPassDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder?.ApplyConfiguration<UserJwtToken>(new UserJwtTokenConfiguration());

            modelBuilder?.ApplyConfiguration<AppUser>(new AppUserConfiguration());

            modelBuilder?.ApplyConfiguration<Password>(new PasswordConfiguration());

            modelBuilder?.ApplyConfiguration<Note>(new NoteConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
