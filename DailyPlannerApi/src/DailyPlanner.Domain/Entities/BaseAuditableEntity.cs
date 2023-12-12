namespace DailyPlanner.Domain.Entities
{
    public abstract class BaseAuditableEntity : IAuditableEntity
    {
        public Guid Id { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}