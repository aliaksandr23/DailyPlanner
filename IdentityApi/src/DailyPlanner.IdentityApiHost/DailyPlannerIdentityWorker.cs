using DailyPlanner.IdentityApiHost.Data;

namespace DailyPlanner.IdentityApiHost
{
    public class DailyPlannerIdentityWorker : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        public DailyPlannerIdentityWorker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            await using var scope = _serviceProvider.CreateAsyncScope();

            var _initializer = scope.ServiceProvider.GetRequiredService<DailyPlannerIdentityDbInitializer>();

            await _initializer.InitializeAsync(cancellationToken);
            await _initializer.SeedAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
    }
}