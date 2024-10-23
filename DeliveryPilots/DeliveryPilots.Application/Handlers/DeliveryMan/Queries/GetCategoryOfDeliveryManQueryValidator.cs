using DeliveryPilots.Domain.Resources;
using FluentValidation;

namespace DeliveryPilots.Application.Handlers.DeliveryMan.Queries;

public class GetCategoryOfDeliveryManQueryValidator : AbstractValidator<GetCategoryOfDeliveryManQuery>
{
    public GetCategoryOfDeliveryManQueryValidator()
    {
        RuleFor(x => x.Identificador)
            .NotEmpty().WithMessage(Messages.InvalidId)
            .MinimumLength(1).WithMessage(Messages.InvalidId);
    }
}
