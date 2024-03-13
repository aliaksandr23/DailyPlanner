using DailyPlanner.Domain.Exceptions;

namespace DailyPlanner.Infrastructure.Exceptions;

public class EntityNotFoundException : DailyPlannerException
{
    public EntityNotFoundException(Type entityType)
        : base(typeof(EntityNotFoundException), $"{entityType.Name} entity is missing")
    { }
}