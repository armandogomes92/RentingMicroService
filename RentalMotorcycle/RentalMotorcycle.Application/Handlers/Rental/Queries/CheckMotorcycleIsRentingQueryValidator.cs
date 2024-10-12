using FluentValidation;
using RentalMotorcycle.Domain.Resources;

namespace RentalMotorcycle.Application.Handlers.Rental.Queries;

public class CheckMotorcycleIsRentingQueryValidator : AbstractValidator<CheckMotorcycleIsRentingQuery>
{
    public CheckMotorcycleIsRentingQueryValidator()
    {
        RuleFor(x => x.Identificador)
            .NotEmpty()
            .WithMessage(Messages.InvalidData);
    }
}