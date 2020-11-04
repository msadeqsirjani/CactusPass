using Domain.Entities;
using Infrastructure.Data.Persistence.Configuration.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Persistence.Configuration
{
    public class NoteConfiguration : EntityConfiguration<Note>
    {
        public override void Configure(EntityTypeBuilder<Note> builder)
        {
            base.Configure(builder);

            builder?.Property(n => n.Title)
                .IsRequired()
                .HasMaxLength(256);

            builder?.Property(n => n.Body)
                .IsRequired()
                .HasMaxLength(2048);

            builder?.HasOne(n => n.User)
                .WithMany(n => n.Notes)
                .HasForeignKey(n => n.UserId)
                .IsRequired();
        }
    }
}
