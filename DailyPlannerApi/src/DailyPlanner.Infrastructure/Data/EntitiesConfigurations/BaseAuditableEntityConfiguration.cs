using Microsoft.EntityFrameworkCore;
using DailyPlanner.Domain.Configuration;
using DailyPlanner.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyPlanner.Infrastructure.Data.EntitiesConfigurations;

internal abstract class BaseAuditableEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> 
    where TEntity : BaseAuditableEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(e => e.CreatedBy).IsRequired();
        builder.Property(e => e.UpdatedOn).HasColumnType(EntitiesConfigurationConstants.DateTimeFormat);
        builder.Property(e => e.CreatedOn).IsRequired().HasColumnType(EntitiesConfigurationConstants.DateTimeFormat);
    }
}