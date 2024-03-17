using DailyPlanner.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static DailyPlanner.Domain.Configuration.EntitiesConfigurationConstants;

namespace DailyPlanner.Infrastructure.Data.EntitiesConfigurations;

internal class BoardConfiguration : BaseAuditableEntityConfiguration<Board>
{
    public override void Configure(EntityTypeBuilder<Board> builder)
    {
        base.Configure(builder);
        builder.Property(b => b.IsPrivate).IsRequired();
        builder.Property(b => b.IsFavorite).IsRequired();
        builder.Property(b => b.Title).IsRequired().HasMaxLength(BoardConstants.MaxTitleLength);
    }
}