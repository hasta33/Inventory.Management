using FluentValidation;
using InventoryManagement.Core.DTOs.Inventory;

namespace InventoryManagement.Services.Validations.Inventory
{
    public class InventoryEmbezzledDtoValidator : AbstractValidator<InventoryEmbezzledDto>
    {
        public InventoryEmbezzledDtoValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("{PropertyName} bu alan gereklidir").NotEmpty().WithMessage("{PropertyName} bu alan gereklidir");
            RuleFor(x => x.Responsible).NotNull().WithMessage("{PropertyName} bu alan gereklidir").NotEmpty().WithMessage("{PropertyName} bu alan gereklidir");
        }
    }
}
