using MediatR;

namespace DailyPlanner.Application.CQRS.Abstractions.Commands
{
    public interface ICommand<out TResponse> : IRequest<TResponse> { }
}