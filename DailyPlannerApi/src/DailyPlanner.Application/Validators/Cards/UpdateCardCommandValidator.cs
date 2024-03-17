using FluentValidation;
using DailyPlanner.Application.CQRS.Cards.Commands;
using static DailyPlanner.Domain.Configuration.EntitiesConfigurationConstants;

namespace DailyPlanner.Application.Validators.Cards;

internal class UpdateCardCommandValidation : AbstractValidator<UpdateCardCommand>
{
    public UpdateCardCommandValidation()
    {
        RuleFor(c => c.Title)
            .NotEmpty()
            .WithMessage("Please enter card title")
            .MaximumLength(CardConstants.MaxTitleLength)
            .WithMessage($"Card title must not exceed {CardConstants.MaxTitleLength} characters");
        RuleFor(c => c.Description)
            .NotNull()
            .WithMessage("Card description can't be null")
            .MaximumLength(CardConstants.MaxDescriptionLength)
            .WithMessage($"Card description must not exceed {CardConstants.MaxDescriptionLength} characters");
        RuleFor(c => c.StartDate)
            .LessThan(c => c.EndDate)
            .When(c => c.StartDate is not null && c.EndDate is not null)
            .WithMessage("Start date must be less than end date");
    }
}