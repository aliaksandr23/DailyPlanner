using DailyPlanner.Infrastructure.Data;

namespace DailyPlanner.ApiHost
{
    public class DailyPlannerWorker : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        public DailyPlannerWorker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            await using var scope = _serviceProvider.CreateAsyncScope();

            var _initializer = scope.ServiceProvider.GetRequiredService<DailyPlannerDbInitializer>();

            await _initializer.InitializeAsync(cancellationToken);
            await _initializer.SeedAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
    }
}