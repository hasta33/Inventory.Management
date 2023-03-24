using FluentValidation;
using InventoryManagement.Core.DTOs.InventoryMovement;

namespace InventoryManagement.Services.Validations.InventoryMovement
{
    public class InventorymovementCreateDtoValidator : AbstractValidator<InventoryMovementCreateDto>
    {
        public InventorymovementCreateDtoValidator()
        {
            RuleFor(x => x.InventoryId).NotNull().WithMessage("{PropertyName} bu alan gereklidir").NotEmpty().WithMessage("{PropertyName} bu alan gereklidir");
        }
    }
}
