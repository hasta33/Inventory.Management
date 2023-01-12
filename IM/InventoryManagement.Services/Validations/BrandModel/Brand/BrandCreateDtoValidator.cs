using FluentValidation;
using InventoryManagement.Core.DTOs.BrandModel.Brand;

namespace InventoryManagement.Services.Validations.BrandModel.Brand
{
    public class BrandCreateDtoValidator : AbstractValidator<BrandCreateDto>
    {
        public BrandCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} bu alan gereklidir.").NotEmpty().WithMessage("{PropertyName} bu alan gereklidir.");
            RuleFor(x => x.BusinessCode).NotNull().WithMessage("{PropertyName} bu alan gereklidir.").NotEmpty().WithMessage("{PropertyName} bu alan gereklidir.");
        }
    }
}
