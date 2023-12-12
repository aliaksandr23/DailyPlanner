using DailyPlanner.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using DailyPlanner.Domain.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyPlanner.Infrastructure.Data.EntitiesConfigurations
{
    internal class ColumnConfiguration : IEntityTypeConfiguration<Column>
    {
        public void Configure(EntityTypeBuilder<Column> builder)
        {
            builder.Property(c => c.CreatedBy).IsRequired();
            builder.Property(c => c.Title).IsRequired().HasMaxLength(EntitiesConfigurationConstants.MaxColumnTitleLength);
            builder.Property(c => c.UpdatedOn).HasColumnType("smalldatetime");
            builder.Property(c => c.CreatedOn).IsRequired().HasColumnType("smalldatetime");
        }
    }
}