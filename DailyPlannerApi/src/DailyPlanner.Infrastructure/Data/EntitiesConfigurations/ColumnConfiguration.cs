using DailyPlanner.Domain.Entities;
using DailyPlanner.Domain.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyPlanner.Infrastructure.Data.EntitiesConfigurations;

internal class ColumnConfiguration : BaseAuditableEntityConfiguration<Column>
{
    public override void Configure(EntityTypeBuilder<Column> builder)
    {
        base.Configure(builder);
        builder.Property(c => c.CreatedBy).IsRequired();
        builder.Property(c => c.Title).IsRequired().HasMaxLength(EntitiesConfigurationConstants.MaxColumnTitleLength);
    }
}