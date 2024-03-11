using MediatR;
using Microsoft.AspNetCore.Mvc;
using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Application.CQRS.Cards.Queries.GetById;
using DailyPlanner.Application.CQRS.Cards.Commands.Create;
using DailyPlanner.Application.CQRS.Cards.Commands.Delete;
using DailyPlanner.Application.CQRS.Cards.Commands.Update;

namespace DailyPlanner.ApiHost.Controllers
{
    public class CardController : BaseController
    {
        public CardController(ISender sender) : base(sender) { }

        [HttpGet("[action]")]
        public async Task<ActionResult<CardDto>> GetById(Guid id, Guid boardId)
        {
            var response = await Sender.Send(new GetByIdCardQuery() { Id = id, BoardId = boardId });
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<CardDto>> Create([FromBody] CreateCardCommand createCardCommand)
        {
            var response = await Sender.Send(createCardCommand);
            return Ok(response);
        }

        [HttpPatch("[action]")]
        public async Task<ActionResult> Update([FromBody] UpdateCardCommand updateCardCommand)
        {
            await Sender.Send(updateCardCommand);
            return NoContent();
        }

        [HttpDelete("[action]")]
        public async Task<ActionResult> Delete([FromBody] DeleteCardCommand deleteCardCommand)
        {
            await Sender.Send(deleteCardCommand);
            return NoContent();
        }
    }
}