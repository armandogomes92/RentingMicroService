using MotorcycleService.Domain.Resources;
using FluentValidation;

namespace MotorcycleService.Application.Handlers.Motorcycle.Commands.Create
{
    public class CreateMotorcycleCommandValidator : AbstractValidator<CreateMotorcycleCommand>
    {
        public CreateMotorcycleCommandValidator()
        {
            RuleFor(query => query.Identificador)
            .NotEmpty()
            .NotNull()
            .WithMessage(Messages.IvalidData);

            RuleFor(query => query.Ano)
            .NotEmpty()
            .NotNull()
            .WithMessage(Messages.IvalidData);

            RuleFor(query => query.Placa)
            .NotEmpty()
            .NotNull()
            .WithMessage(Messages.IvalidData);

            RuleFor(query => query.Modelo)
            .NotEmpty()
            .NotNull()
            .WithMessage(Messages.IvalidData);
        }
    }
}
