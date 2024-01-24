using MediatR;
using Microsoft.AspNetCore.Mvc;
using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Infrastructure.Services.User;
using DailyPlanner.Application.CQRS.Columns.Commands.Create;
using DailyPlanner.Application.CQRS.Columns.Commands.Delete;
using DailyPlanner.Application.CQRS.Columns.Commands.Update;

namespace DailyPlanner.ApiHost.Controllers
{
    public class ColumnController : BaseController
    {
        public ColumnController(IUserService userService, ISender sender)
            : base(userService, sender) { }

        [HttpPost("[action]")]
        public async Task<ActionResult<ColumnDto>> Create([FromBody] CreateColumnCommand createCardCommand)
        {
            var response = await Sender.Send(createCardCommand);
            return Ok(response);
        }

        [HttpPatch("[action]")]
        public async Task<ActionResult> Update([FromBody] UpdateColumnCommand updateColumnCommand)
        {
            await Sender.Send(updateColumnCommand);
            return NoContent();
        }

        [HttpDelete("[action]")]
        public async Task<ActionResult> Delete([FromBody] DeleteColumnCommand deleteColumnCommand)
        {
            await Sender.Send(deleteColumnCommand);
            return NoContent();
        }
    }
}