using DailyPlanner.Infrastructure.Services.User;

namespace DailyPlanner.ApiHost.Services.User
{
    internal class DevUserService : IUserService
    {
        public Guid UserId => new("F1DDF39A-41C5-40EA-8310-2D855DC6A192");
    }
}