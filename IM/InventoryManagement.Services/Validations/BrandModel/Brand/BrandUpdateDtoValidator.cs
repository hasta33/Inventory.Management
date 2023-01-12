using FluentValidation;
using InventoryManagement.Core.DTOs.BrandModel.Brand;

namespace InventoryManagement.Services.Validations.BrandModel.Brand
{
    public class BrandUpdateDtoValidator : AbstractValidator<BrandUpdateDto>
    {
        public BrandUpdateDtoValidator() 
        {
            RuleFor(x => x.Id).NotNull().WithMessage("{PropertyName} bu alan gereklidir.").NotEmpty().WithMessage("{PropertyName} bu alan gereklidir.");
            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} bu alan gereklidir.").NotEmpty().WithMessage("{PropertyName} bu alan gereklidir.");
            RuleFor(x => x.BusinessCode).NotNull().WithMessage("{PropertyName} bu alan gereklidir.").NotEmpty().WithMessage("{PropertyName} bu alan gereklidir.");
        }
    }
}
