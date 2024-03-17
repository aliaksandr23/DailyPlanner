using MediatR;
using Microsoft.AspNetCore.Mvc;
using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Application.CQRS.Columns.Commands;

namespace DailyPlanner.ApiHost.Controllers;

public class ColumnController : BaseController
{
    public ColumnController(ISender sender) : base(sender) { }

    [HttpPost("[action]")]
    public async Task<ActionResult<ColumnDto>> Create([FromBody] CreateColumnCommand createColumnCommand)
    {
        var response = await Sender.Send(createColumnCommand);
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