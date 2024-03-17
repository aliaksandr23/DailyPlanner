using FluentValidation;
using DailyPlanner.Application.CQRS.ToDoLists.Commands;
using static DailyPlanner.Domain.Configuration.EntitiesConfigurationConstants;

namespace DailyPlanner.Application.Validators.ToDoLists;

internal class UpdateToDoListCommandValidator : AbstractValidator<UpdateToDoListCommand>
{
    public UpdateToDoListCommandValidator()
    {
        RuleFor(l => l.Title)
            .NotEmpty()
            .WithMessage("Please enter List title")
            .MaximumLength(ToDoListConstants.MaxTitleLength)
            .WithMessage($"List title must not exceed {ToDoListConstants.MaxTitleLength} characters");
    }
}