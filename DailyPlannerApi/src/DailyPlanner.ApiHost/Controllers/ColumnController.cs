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

        [HttpPost]
        public async Task<ActionResult<ColumnDto>> CreateColumn([FromBody] CreateColumnCommand createCardCommand)
        {
            var response = await Sender.Send(createCardCommand);
            return Ok(response);
        }

        [HttpPatch]
        public async Task<ActionResult> UpdateColumn([FromBody] UpdateColumnCommand updateColumnCommand)
        {
            await Sender.Send(updateColumnCommand);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteColumn([FromBody] DeleteColumnCommand deleteColumnCommand)
        {
            await Sender.Send(deleteColumnCommand);
            return NoContent();
        }
    }
}