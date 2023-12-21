using MediatR;
using Microsoft.AspNetCore.Mvc;
using DailyPlanner.Application.Common.DTO;
using DailyPlanner.Infrastructure.Services.CurrentUser;
using DailyPlanner.Application.CQRS.Cards.Queries.GetById;
using DailyPlanner.Application.CQRS.Cards.Commands.Create;
using DailyPlanner.Application.CQRS.Cards.Commands.Delete;
using DailyPlanner.Application.CQRS.Cards.Commands.Update;

namespace DailyPlanner.ApiHost.Controllers
{
    public class CardController : BaseController
    {
        public CardController(ICurrentUserService userService, ISender sender)
            : base(userService, sender)
        { }

        [HttpGet]
        public async Task<ActionResult<CardDto>> GetCard(Guid id)
        {
            var getCardQuery = new GetByIdCardQuery() { Id = id };
            var response = await Sender.Send(getCardQuery);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<CardDto>> CreateCard([FromBody] CreateCardCommand createCardCommand)
        {
            var response = await Sender.Send(createCardCommand);
            return Ok(response);
        }

        [HttpPatch]
        public async Task<ActionResult> UpdateCard([FromBody] UpdateCardCommand updateCardCommand)
        {
            await Sender.Send(updateCardCommand);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCard([FromBody] DeleteCardCommand deleteCardCommand)
        {
            await Sender.Send(deleteCardCommand);
            return NoContent();
        }
    }
}