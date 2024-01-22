namespace DailyPlanner.Domain.Entities
{
    public interface IAuditableEntity
    {
        public Guid CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}