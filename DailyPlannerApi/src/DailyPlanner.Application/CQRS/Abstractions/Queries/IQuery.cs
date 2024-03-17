using MediatR;

namespace DailyPlanner.Application.CQRS.Abstractions.Queries;

public interface IQuery<out TResponse> : IRequest<TResponse> { }