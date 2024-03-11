using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DailyPlanner.ApiHost.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BaseController : ControllerBase
    {
        protected ISender Sender { get; }

        public BaseController(ISender sender)
        {
            Sender = sender;
        }
    }
}