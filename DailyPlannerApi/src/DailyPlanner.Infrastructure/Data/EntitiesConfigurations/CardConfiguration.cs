using DailyPlanner.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static DailyPlanner.Domain.Configuration.EntitiesConfigurationConstants;

namespace DailyPlanner.Infrastructure.Data.EntitiesConfigurations;

internal class CardConfiguration : BaseAuditableEntityConfiguration<Card>
{
    public override void Configure(EntityTypeBuilder<Card> builder)
    {
        base.Configure(builder);
        builder.OwnsOne(c => c.CardDateSection, cd =>
        {
            cd.Property(d => d.IsDone).IsRequired().HasColumnName("IsDone");
            cd.Property(d => d.EndDate).HasColumnType(DateTimeFormat).HasColumnName("EndDate");
            cd.Property(d => d.StartDate).HasColumnType(DateTimeFormat).HasColumnName("StartDate");
        });
        builder.Property(c => c.Priority).IsRequired().HasConversion<string>();
        builder.Property(c => c.Title).IsRequired().HasMaxLength(CardConstants.MaxTitleLength);
        builder.Property(c => c.Description).IsRequired().HasMaxLength(CardConstants.MaxDescriptionLength);
    }
}