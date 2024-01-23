using MediatR;
using Microsoft.AspNetCore.Mvc;
using DailyPlanner.ApiHost.ViewModels;
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

        [HttpGet]
        [Route("Board/id:guid")]
        public async Task<ActionResult<BoardDto>> GetBoard(Guid id)
        {
            var getBoardQuery = new GetByIdBoardQuery() { Id = id, };
            var response = await Sender.Send(getBoardQuery);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<BoardsVM>> GetBoards()
        {
            var getBoardsQuery = new GetAllBoardsQuery();
            var response = await Sender.Send(getBoardsQuery);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<BoardDto>> CreateBoard([FromBody] CreateBoardCommand createBoardCommand)
        {
            var response = await Sender.Send(createBoardCommand);
            return Ok(response);
        }

        [HttpPatch]
        public async Task<ActionResult> UpdateBoard([FromBody] UpdateBoardCommand updateBoardCommand)
        {
            await Sender.Send(updateBoardCommand);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteBoard([FromBody] DeleteBoardCommand deleteBoardCommand)
        {
            await Sender.Send(deleteBoardCommand);
            return NoContent();
        }
    }
}