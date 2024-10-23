using FluentValidation;
using RentalMotorcycle.Domain.Resources;

namespace RentalMotorcycle.Application.Handlers.Rental.Commands.Update;

public class UpdateRentalRegistryCommandValidator : AbstractValidator<UpdateRentalRegistryCommand>
{
    public UpdateRentalRegistryCommandValidator()
    {
        RuleFor(x => x.DataDevolucao)
            .NotEmpty()
            .WithMessage(Messages.InvalidData);
    }
}
