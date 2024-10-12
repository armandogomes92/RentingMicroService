using MotorcycleService.Domain.Resources;
using FluentValidation;

namespace MotorcycleService.Application.Handlers.Motorcycle.Commands.Delete;

public class DeleteMotorcycleByIdCommandValidator : AbstractValidator<DeleteMotorcycleByIdCommand>
{
    public DeleteMotorcycleByIdCommandValidator()
    {
        RuleFor(query => query.Id)
            .NotEmpty()
            .NotNull()
            .WithMessage(Messages.IvalidData);
    }
}