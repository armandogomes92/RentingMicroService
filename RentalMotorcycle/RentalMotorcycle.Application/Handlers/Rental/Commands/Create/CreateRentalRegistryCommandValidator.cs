using FluentValidation;
using RentalMotorcycle.Domain.Resources;

namespace RentalMotorcycle.Application.Handlers.Rental.Commands.Create
{
    public class CreateRentalRegistryCommandValidator
        : AbstractValidator<CreateRentalRegistryCommand>
    {
        public CreateRentalRegistryCommandValidator()
        {
            RuleFor(x => x.EntregadorId)
                .NotEmpty().WithMessage(Messages.InvalidData)
                .MinimumLength(1).WithMessage(Messages.InvalidData);

            RuleFor(x => x.MotoId)
                .NotEmpty().WithMessage(Messages.InvalidData)
                .MinimumLength(3).WithMessage(Messages.InvalidData);

            RuleFor(x => x.DataInicio)
                .NotEmpty().WithMessage(Messages.InvalidData);

            RuleFor(x => x.DataTermino)
                .NotEmpty().WithMessage(Messages.InvalidData);

            RuleFor(x => x.DataPrevisaoTermino)
                .NotEmpty().WithMessage(Messages.InvalidData);

            RuleFor(x => x.Plano)
                .NotEmpty().WithMessage(Messages.InvalidData);
        }
    }
}
