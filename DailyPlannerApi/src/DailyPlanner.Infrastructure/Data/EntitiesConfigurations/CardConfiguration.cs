using DailyPlanner.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using DailyPlanner.Domain.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyPlanner.Infrastructure.Data.EntitiesConfigurations
{
    internal class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.Property(c => c.CreatedBy).IsRequired();
            builder.Property(c => c.Priority).HasConversion<string>();
            builder.Property(c => c.Description).HasMaxLength(EntitiesConfigurationConstants.MaxCardDescriptionLength);
            builder.Property(c => c.Title).IsRequired().HasMaxLength(EntitiesConfigurationConstants.MaxBoardTitleLength);
            builder.Property(c => c.EndDate).HasColumnType("smalldatetime");
            builder.Property(c => c.StartDate).HasColumnType("smalldatetime");
            builder.Property(c => c.UpdatedOn).HasColumnType("smalldatetime");
            builder.Property(c => c.CreatedOn).IsRequired().HasColumnType("smalldatetime");
        }
    }
}