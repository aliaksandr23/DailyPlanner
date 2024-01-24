using MediatR;
using Microsoft.AspNetCore.Mvc;
using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Infrastructure.Services.User;
using DailyPlanner.Application.CQRS.Boards.Queries.GetAll;
using DailyPlanner.Application.CQRS.Boards.Commands.Create;
using DailyPlanner.Application.CQRS.Boards.Commands.Delete;
using DailyPlanner.Application.CQRS.Boards.Commands.Update;
using DailyPlanner.Application.CQRS.Boards.Queries.GetById;

namespace DailyPlanner.ApiHost.Controllers
{
    public class BoardController : BaseController
    {
        public BoardController(IUserService userService, ISender sender)
            : base(userService, sender) { }

        [HttpGet("[action]:Guid")]
        public async Task<ActionResult<BoardDto>> GetById(Guid id)
        {
            var response = await Sender.Send(new GetByIdBoardQuery() { Id = id, });
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<BoardDto>>> GetAll()
        {
            var response = await Sender.Send(new GetAllBoardsQuery());
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<BoardDto>> Create([FromBody] CreateBoardCommand createBoardCommand)
        {
            var response = await Sender.Send(createBoardCommand);
            return Ok(response);
        }

        [HttpPatch("[action]")]
        public async Task<ActionResult> Update([FromBody] UpdateBoardCommand updateBoardCommand)
        {
            await Sender.Send(updateBoardCommand);
            return NoContent();
        }

        [HttpDelete("[action]")]
        public async Task<ActionResult> Delete([FromBody] DeleteBoardCommand deleteBoardCommand)
        {
            await Sender.Send(deleteBoardCommand);
            return NoContent();
        }
    }
}