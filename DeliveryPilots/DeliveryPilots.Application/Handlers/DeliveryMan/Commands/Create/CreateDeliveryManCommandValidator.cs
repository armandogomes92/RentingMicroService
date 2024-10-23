using DeliveryPilots.Domain.Resources;
using FluentValidation;

namespace DeliveryPilots.Application.Handlers.DeliveryMan.Commands.Create;

public class CreateDeliveryManCommandValidator : AbstractValidator<CreateDeliveryManCommand>
{
    public CreateDeliveryManCommandValidator()
    {
        RuleFor(x => x.Identificador)
            .NotEmpty().WithMessage(Messages.InvalidId)
            .MinimumLength(1).WithMessage(Messages.InvalidId);

        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage(Messages.InvalidName)
            .MinimumLength(3).WithMessage(Messages.InvalidName);

        RuleFor(x => x.Cnpj)
            .NotEmpty().WithMessage(Messages.InvalidCnpj)
            .Length(11).WithMessage(Messages.InvalidCnpj);

        RuleFor(x => x.DataNascimento)
            .NotEmpty().WithMessage(Messages.InvalidBirthDate);

        RuleFor(x => x.NumeroCnh)
            .NotEmpty().WithMessage(Messages.InvalidCnhNumber)
            .MinimumLength(6).WithMessage(Messages.InvalidCnhNumber);

        RuleFor(x => x.TipoCnh)
            .NotEmpty().WithMessage(Messages.InvalidTypeOfCnh)
            .Must(tipo => tipo != null && (tipo.ToUpper() == "A" || tipo.ToUpper() == "B" || tipo.ToUpper() == "AB"))
            .WithMessage(Messages.InvalidTypeOfCnh);

        RuleFor(x => x.ImagemCnh)
            .NotEmpty().WithMessage(Messages.InvalidCnhImage)
            .Must(IsValidImage)
            .WithMessage(Messages.InvalidCnhImage);
    }

    private static bool IsValidImage(byte[] fileBytes)
    {
        if (fileBytes == null || fileBytes.Length < 4)
        {
            return false;
        }

        // Verifica se é um arquivo PNG
        if (fileBytes[0] == 0x89 && fileBytes[1] == 0x50 && fileBytes[2] == 0x4E && fileBytes[3] == 0x47)
        {
            return true;
        }

        // Verifica se é um arquivo BMP
        if (fileBytes[0] == 0x42 && fileBytes[1] == 0x4D)
        {
            return true;
        }

        return false;
    }
}