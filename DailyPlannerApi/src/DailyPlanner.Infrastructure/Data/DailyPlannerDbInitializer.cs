using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using DailyPlanner.Infrastructure.Services.DateAndTime;

namespace DailyPlanner.Infrastructure.Data;

public class DailyPlannerDbInitializer
{
    private readonly DailyPlannerDbContext _context;
    private readonly ILogger<DailyPlannerDbInitializer> _logger;

    public DailyPlannerDbInitializer(DailyPlannerDbContext context,
        ILogger<DailyPlannerDbInitializer> logger, IDateTimeService dateTimeService)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.Database.MigrateAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initializing the database");
            throw;
        }
    }
}