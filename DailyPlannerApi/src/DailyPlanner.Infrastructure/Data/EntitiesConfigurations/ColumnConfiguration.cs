using DailyPlanner.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static DailyPlanner.Domain.Configuration.EntitiesConfigurationConstants;

namespace DailyPlanner.Infrastructure.Data.EntitiesConfigurations;

internal class ColumnConfiguration : BaseAuditableEntityConfiguration<Column>
{
    public override void Configure(EntityTypeBuilder<Column> builder)
    {
        base.Configure(builder);
        builder.Property(c => c.Title).IsRequired().HasMaxLength(ColumnConstants.MaxTitleLength);
    }
}