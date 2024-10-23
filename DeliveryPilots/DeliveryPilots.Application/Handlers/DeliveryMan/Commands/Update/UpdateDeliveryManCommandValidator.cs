using DeliveryPilots.Domain.Resources;
using FluentValidation;

namespace DeliveryPilots.Application.Handlers.DeliveryMan.Commands.Update;

public class UpdateDeliveryManCommandValidator : AbstractValidator<UpdateDeliveryManCommand>
{
    public UpdateDeliveryManCommandValidator()
    {
        RuleFor(x => x.Identificador)
            .NotEmpty().WithMessage(Messages.InvalidId);

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