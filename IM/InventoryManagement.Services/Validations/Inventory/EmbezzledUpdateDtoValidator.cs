using FluentValidation;
using InventoryManagement.Core.DTOs.Inventory;

namespace InventoryManagement.Services.Validations.Inventory
{
    public class EmbezzledUpdateDtoValidator : AbstractValidator<EmbezzledUpdateDto>
    {
        public EmbezzledUpdateDtoValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("{PropertyName} bu alan gereklidir").NotEmpty().WithMessage("{PropertyName} bu alan gereklidir");
            RuleFor(x => x.Embezzled).NotNull().WithMessage("{PropertyName} bu alan gereklidir").NotEmpty().WithMessage("{PropertyName} bu alan gereklidir");
        }
    }
}
