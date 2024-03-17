using MediatR;
using Microsoft.AspNetCore.Mvc;
using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Application.CQRS.ToDoLists.Commands;

namespace DailyPlanner.ApiHost.Controllers;

public class ToDoListController : BaseController
{
    public ToDoListController(ISender sender) : base(sender) { }

    [HttpPost("[action]")]
    public async Task<ActionResult<ToDoListDto>> Create([FromBody] CreateToDoListCommand createToDoListCommand)
    {
        var response = await Sender.Send(createToDoListCommand);
        return Ok(response);
    }

    [HttpPatch("[action]")]
    public async Task<ActionResult> Update([FromBody] UpdateToDoListCommand updateToDoListCommand)
    {
        await Sender.Send(updateToDoListCommand);
        return NoContent();
    }

    [HttpDelete("[action]")]
    public async Task<ActionResult> Delete([FromBody] DeleteToDoListCommand deleteToDoListCommand)
    {
        await Sender.Send(deleteToDoListCommand);
        return NoContent();
    }
}