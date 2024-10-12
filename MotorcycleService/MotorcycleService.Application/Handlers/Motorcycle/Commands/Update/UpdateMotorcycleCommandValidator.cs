using MotorcycleService.Domain.Resources;
using FluentValidation;

namespace MotorcycleService.Application.Handlers.Motorcycle.Commands.Update;

public class UpdateMotorcycleCommandValidator : AbstractValidator<UpdateMotorcycleCommand>
{
    public UpdateMotorcycleCommandValidator()
    {
        RuleFor(query => query.Identificador)
            .NotEmpty()
            .NotNull()
            .WithMessage(Messages.IvalidData);

        RuleFor(query => query.Placa)
        .NotEmpty()
        .NotNull()
        .WithMessage(Messages.IvalidData);
    }
}