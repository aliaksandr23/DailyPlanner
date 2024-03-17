using DailyPlanner.Domain.Exceptions;

namespace DailyPlanner.Application.Exceptions;

public class EntityValidationException : DailyPlannerException
{
    public EntityValidationException(IReadOnlyDictionary<string, string[]> errorsDictionary)
        : base(typeof(EntityValidationException), "One or more validation failures have occurred.")
    {
        ErrorsDictionary = errorsDictionary;
    }

    public IReadOnlyDictionary<string, string[]> ErrorsDictionary { get; }
}