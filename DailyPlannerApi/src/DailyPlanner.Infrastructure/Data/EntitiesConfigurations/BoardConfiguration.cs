using DailyPlanner.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using DailyPlanner.Domain.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyPlanner.Infrastructure.Data.EntitiesConfigurations
{
    internal class BoardConfiguration : IEntityTypeConfiguration<Board>
    {
        public void Configure(EntityTypeBuilder<Board> builder)
        {
            builder.Property(b => b.CreatedBy).IsRequired();
            builder.Property(b => b.IsPrivate).IsRequired();
            builder.Property(b => b.IsFavorite).IsRequired();
            builder.Property(b => b.Title).IsRequired().HasMaxLength(EntitiesConfigurationConstants.MaxBoardTitleLength);
            builder.Property(b => b.UpdatedOn).HasColumnType("smalldatetime");
            builder.Property(b => b.CreatedOn).IsRequired().HasColumnType("smalldatetime");
        }
    }
}