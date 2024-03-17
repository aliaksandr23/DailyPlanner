using DailyPlanner.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static DailyPlanner.Domain.Configuration.EntitiesConfigurationConstants;

namespace DailyPlanner.Infrastructure.Data.EntitiesConfigurations;

internal class ToDoItemConfiguration : BaseAuditableEntityConfiguration<ToDoItem>
{
    public override void Configure(EntityTypeBuilder<ToDoItem> builder)
    {
        base.Configure(builder);
        builder.Property(i => i.IsDone).IsRequired();
        builder.Property(i => i.Title).IsRequired().HasMaxLength(ToDoItemConstants.MaxTitleLength);
    }
}