﻿using MediatR;
using FluentValidation;
using DailyPlanner.Application.Exceptions;
using DailyPlanner.Application.CQRS.Abstractions.Commands;

namespace DailyPlanner.Application.Behaviours
{
    public class ValidationPipelineBehaviours<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : class, ICommand<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationPipelineBehaviours(IEnumerable<IValidator<TRequest>> validators) 
            => _validators = validators;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);

            var errorsDictionary = _validators
                .Select(x => x.Validate(context))
                .SelectMany(x => x.Errors)
                .Where(x => x is not null)
                .GroupBy(
                    x => x.PropertyName,
                    x => x.ErrorMessage,
                    (propertyName, errorMessages) => new
                    {
                        Key = propertyName,
                        Values = errorMessages.Distinct().ToArray()
                    })
                .ToDictionary(x => x.Key, x => x.Values);

            if (errorsDictionary.Any())
            {
                throw new EntityValidationException(errorsDictionary);
            }

            return await next();
        }
    }
}