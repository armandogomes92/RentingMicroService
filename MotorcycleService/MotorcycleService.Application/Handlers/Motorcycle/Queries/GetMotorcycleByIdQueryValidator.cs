using MotorcycleService.Domain.Resources;
using FluentValidation;

namespace MotorcycleService.Application.Handlers.Motorcycle.Queries;

public class GetMotorcycleByIdQueryValidator : AbstractValidator<GetMotorcycleByIdQuery>
{
    public GetMotorcycleByIdQueryValidator()
    {
        RuleFor(query => query.Id)
        .NotEmpty()
        .NotNull()
        .WithMessage(Messages.BadRequest);
    }
}