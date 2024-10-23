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
                .NotEmpty().WithMessage(Messages.InvalidDeliveryManId)
                .MinimumLength(1).WithMessage(Messages.InvalidDeliveryManId);

            RuleFor(x => x.MotoId)
                .NotEmpty().WithMessage(Messages.InvalidMotorcycleId)
                .MinimumLength(3).WithMessage(Messages.InvalidMotorcycleId);

            RuleFor(x => x.DataInicio)
                .NotEmpty().WithMessage(Messages.InvalidStartDate);

            RuleFor(x => x.DataTermino)
                .NotEmpty().WithMessage(Messages.InvalidEndDate);

            RuleFor(x => x.DataPrevisaoTermino)
                .NotEmpty().WithMessage(Messages.InvalidExpectedEndDate);

            RuleFor(x => x.Plano)
                .NotEmpty().WithMessage(Messages.PlanUnavaliable)
                .Must(plan => plan == 7 || plan == 15 || plan == 30 || plan == 45 || plan == 50)
                .WithMessage(Messages.PlanUnavaliable);
        }
    }
}
