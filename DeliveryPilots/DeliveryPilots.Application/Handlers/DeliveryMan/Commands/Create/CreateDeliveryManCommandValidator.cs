using DeliveryPilots.Domain.Resources;
using FluentValidation;

namespace DeliveryPilots.Application.Handlers.DeliveryMan.Commands.Create
{
    public class CreateDeliveryManCommandValidator : AbstractValidator<CreateDeliveryManCommand>
    {
        public CreateDeliveryManCommandValidator()
        {
            RuleFor(x => x.Identificador)
                .NotEmpty().WithMessage(Messages.InvalidData)
                .MinimumLength(1).WithMessage(Messages.InvalidData);

            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage(Messages.InvalidData)
                .MinimumLength(3).WithMessage(Messages.InvalidData);

            RuleFor(x => x.Cnpj)
                .NotEmpty().WithMessage(Messages.InvalidData)
                .Length(11).WithMessage(Messages.InvalidData);

            RuleFor(x => x.DataNascimento)
                .NotEmpty().WithMessage(Messages.InvalidData);

            RuleFor(x => x.NumeroCnh)
                .NotEmpty().WithMessage(Messages.InvalidData)
                .MinimumLength(6).WithMessage(Messages.InvalidData);

            RuleFor(x => x.TipoCnh)
                .NotEmpty().WithMessage(Messages.InvalidData)
                .Must(tipo => tipo.ToUpper() == "A" || tipo.ToUpper() == "B" || tipo.ToUpper() == "AB")
                .WithMessage(Messages.InvalidData);

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
}
