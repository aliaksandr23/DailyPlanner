using DailyPlanner.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using DailyPlanner.Domain.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyPlanner.Infrastructure.Data.EntitiesConfigurations;

internal class CardConfiguration : BaseAuditableEntityConfiguration<Card>
{
    public override void Configure(EntityTypeBuilder<Card> builder)
    {
        base.Configure(builder);
        builder.OwnsOne(c => c.CardDateSection);
        builder.Property(c => c.Priority).IsRequired().HasConversion<string>();
        builder.Property(c => c.Title).IsRequired().HasMaxLength(EntitiesConfigurationConstants.MaxBoardTitleLength);
        builder.Property(c => c.Description).IsRequired().HasMaxLength(EntitiesConfigurationConstants.MaxCardDescriptionLength);
        builder.Property(c => c.CardDateSection.IsDone).IsRequired().HasColumnName("IsDone");
        builder.Property(c => c.CardDateSection.EndDate).HasColumnType("smalldatetime");
        builder.Property(c => c.CardDateSection.StartDate).HasColumnType("smalldatetime");
    }
}