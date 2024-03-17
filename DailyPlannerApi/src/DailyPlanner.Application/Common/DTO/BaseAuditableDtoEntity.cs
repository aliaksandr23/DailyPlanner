namespace DailyPlanner.Application.Common.DTO;

public abstract record class BaseAuditableDtoEntity
{
    public Guid Id { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
}