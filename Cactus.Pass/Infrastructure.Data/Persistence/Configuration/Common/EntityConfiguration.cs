using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Persistence.Configuration.Common
{
    public class EntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder?.HasKey(b => b.Id);

            builder?.Property(b => b.CreateDateTime)
                .IsRequired();

            builder?.Property(b => b.UpdateDateTime)
                .IsRequired();
        }
    }
}
