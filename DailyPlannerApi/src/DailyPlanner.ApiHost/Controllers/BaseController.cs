using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DailyPlanner.Infrastructure.Services.CurrentUser;

namespace DailyPlanner.ApiHost.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BaseController : ControllerBase
    {
        protected ISender Sender { get; }
        private readonly ICurrentUserService _userService;

        protected Guid UserId => _userService.UserId;

        public BaseController(ICurrentUserService userService, ISender sender)
        {
            Sender = sender;
            _userService = userService;
        }
    }
}