using DailyPlanner.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static DailyPlanner.Domain.Configuration.EntitiesConfigurationConstants;

namespace DailyPlanner.Infrastructure.Data.EntitiesConfigurations;

internal class ToDoListConfiguration : BaseAuditableEntityConfiguration<ToDoList>
{
    public override void Configure(EntityTypeBuilder<ToDoList> builder)
    {
        base.Configure(builder);
        builder.Property(l => l.Title).IsRequired().HasMaxLength(ToDoItemConstants.MaxTitleLength);
    }
}