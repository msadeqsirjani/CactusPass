using Domain.Entities;
using Infrastructure.Data.Persistence.Configuration.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Persistence.Configuration
{
    public class UserJwtTokenConfiguration : EntityConfiguration<UserJwtToken>
    {
        public override void Configure(EntityTypeBuilder<UserJwtToken> builder)
        {
            base.Configure(builder);

            builder?.Property(u => u.AccessToken)
                .IsRequired()
                .HasMaxLength(256);

            builder?.Property(u => u.AccessTokenExpireDateTime)
                .IsRequired();

            builder?.Property(u => u.Platform)
                .IsRequired();

            builder?.HasOne(u => u.User)
                .WithMany(u => u.JwtTokens)
                .HasForeignKey(u => u.UserId)
                .IsRequired();
        }
    }
}
