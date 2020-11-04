using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Persistence.Configuration.Identity
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder?.HasMany(u => u.JwtTokens)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId);

            builder?.HasMany(u => u.Passwords)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId);

            builder?.HasMany(u => u.Notes)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId);
        }
    }
}
