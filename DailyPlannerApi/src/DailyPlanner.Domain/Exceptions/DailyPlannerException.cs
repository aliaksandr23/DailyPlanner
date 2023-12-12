namespace DailyPlanner.Domain.Exceptions
{
    public class DailyPlannerException : Exception
    {
        public string Title { get; }

        public DailyPlannerException()
        {
        }

        public DailyPlannerException(string message)
            : base(message)
        {
        }

        public DailyPlannerException(Type type, string message)
            : base(message)
        {
            Title = type.Name;
        }

        public DailyPlannerException(string message, Exception inner)
            : base(message, inner)
        {
            Title = inner.GetType().Name;
        }
    }
}