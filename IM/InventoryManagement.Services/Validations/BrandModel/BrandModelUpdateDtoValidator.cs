using FluentValidation;
using InventoryManagement.Core.DTOs.BrandModel;

namespace InventoryManagement.Services.Validations.BrandModel
{
    public class BrandModelUpdateDtoValidator : AbstractValidator<BrandModelUpdateDto>
    {
        public BrandModelUpdateDtoValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("{PropertyName} bu alan gereklidir.").NotEmpty().WithMessage("{PropertyName} bu alan gereklidir.");
            RuleFor(x => x.Brand).NotNull().WithMessage("{PropertyName} bu alan gereklidir.").NotEmpty().WithMessage("{PropertyName} bu alan gereklidir.");
            RuleFor(x => x.Model).NotNull().WithMessage("{PropertyName} bu alan gereklidir.").NotEmpty().WithMessage("{PropertyName} bu alan gereklidir.");
        }
    }
}
