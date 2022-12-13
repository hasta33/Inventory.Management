using FluentValidation;
using InventoryManagement.Core.DTOs.BrandModel;

namespace InventoryManagement.Services.Validations.BrandModel
{
    public class BrandModelDeleteDtoValidator : AbstractValidator<BrandModelDeleteDto>
    {
        public BrandModelDeleteDtoValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("{PropertyName} bu alan gereklidir.").NotEmpty().WithMessage("{PropertyName} bu alan gereklidir.");
        }
    }
}
