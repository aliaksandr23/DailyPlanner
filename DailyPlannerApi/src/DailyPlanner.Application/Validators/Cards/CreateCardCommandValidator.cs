using FluentValidation;
using DailyPlanner.Domain.Configuration;
using DailyPlanner.Application.CQRS.Cards.Commands.Create;

namespace DailyPlanner.Application.Validators.Cards
{
    public class CreateCardCommandValidator : AbstractValidator<CreateCardCommand>
    {
        public CreateCardCommandValidator()
        {
            RuleFor(c => c.Title).NotEmpty().WithMessage("Please enter card title")
                .MaximumLength(EntitiesConfigurationConstants.MaxCardTitleLength)
                .WithMessage($"Card title must not exceed {EntitiesConfigurationConstants.MaxCardTitleLength} characters");
            RuleFor(c => c.Description).MaximumLength(EntitiesConfigurationConstants.MaxCardDescriptionLength)
                .WithMessage($"Card description must not exceed {EntitiesConfigurationConstants.MaxCardDescriptionLength} characters");
            RuleFor(c => c.StartDate).LessThan(c => c.EndDate).When(c => c.StartDate != null && c.EndDate != null)
                .WithMessage("Start date must be less than end date");
        }
    }
}