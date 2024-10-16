using DeliveryPilots.Domain.Resources;
using FluentValidation;

namespace DeliveryPilots.Application.Handlers.DeliveryMan.Commands.Update;

public class UpdateDeliveryManCommandValidator : AbstractValidator<UpdateDeliveryManCommand>
{
    public UpdateDeliveryManCommandValidator()
    {
        RuleFor(x => x.ImagemCnh)
            .NotEmpty().WithMessage(Messages.InvalidData)
            .Must(IsValidImage)
            .WithMessage(Messages.InvalidData);
    }

    private static bool IsValidImage(byte[] fileBytes)
    {
        if (fileBytes == null || fileBytes.Length < 4)
        {
            return false;
        }
        // Verifica se é um arquivo JPEG
        if (fileBytes[0] == 0xFF && fileBytes[1] == 0xD8 && fileBytes[fileBytes.Length - 2] == 0xFF && fileBytes[fileBytes.Length - 1] == 0xD9)
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
