using MediatR;
using Microsoft.AspNetCore.Mvc;
using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Application.CQRS.ToDoItems.Commands;

namespace DailyPlanner.ApiHost.Controllers;

public class ToDoItemController : BaseController
{
    public ToDoItemController(ISender sender) : base(sender) { }

    [HttpPost("[action]")]
    public async Task<ActionResult<ToDoListDto>> Create([FromBody] CreateToDoItemCommand createToDoItemCommand)
    {
        var response = await Sender.Send(createToDoItemCommand);
        return Ok(response);
    }

    [HttpPatch("[action]")]
    public async Task<ActionResult> Update([FromBody] UpdateToDoItemCommand updateToDoItemCommand)
    {
        await Sender.Send(updateToDoItemCommand);
        return NoContent();
    }

    [HttpDelete("[action]")]
    public async Task<ActionResult> Delete([FromBody] DeleteToDoItemCommand deleteToDoItemCommand)
    {
        await Sender.Send(deleteToDoItemCommand);
        return NoContent();
    }
}