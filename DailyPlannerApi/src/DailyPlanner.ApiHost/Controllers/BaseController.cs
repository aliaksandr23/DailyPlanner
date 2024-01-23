using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DailyPlanner.Infrastructure.Services.User;

namespace DailyPlanner.ApiHost.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BaseController : ControllerBase
    {
        protected ISender Sender { get; }
        private readonly IUserService _userService;

        protected Guid UserId => _userService.UserId;

        public BaseController(IUserService userService, ISender sender)
        {
            Sender = sender;
            _userService = userService;
        }
    }
}