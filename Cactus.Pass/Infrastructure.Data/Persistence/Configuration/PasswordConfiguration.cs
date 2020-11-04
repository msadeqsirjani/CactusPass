using Domain.Entities;
using Infrastructure.Data.Persistence.Configuration.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Persistence.Configuration
{
    public class PasswordConfiguration : EntityConfiguration<Password>
    {
        public override void Configure(EntityTypeBuilder<Password> builder)
        {
            base.Configure(builder);

            builder?.Property(p => p.Username)
                .HasMaxLength(256);

            builder?.Property(p => p.EmailAddress)
                .HasMaxLength(256);

            builder?.Property(p => p.HashedPassword)
                .IsRequired();

            builder?.Property(p => p.UsedIn)
                .IsRequired()
                .HasMaxLength(1000);

            builder.HasOne(p => p.User)
                .WithMany(p => p.Passwords)
                .HasForeignKey(p => p.UserId)
                .IsRequired();
        }
    }
}
