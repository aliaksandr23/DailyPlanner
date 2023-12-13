using Microsoft.AspNetCore.Identity;

namespace DailyPlanner.IdentityApiHost.Data.Entities
{
    public class DailyPlannerUser : IdentityUser
    {
        public int BoardsRemaining { get; set; }
    }
}