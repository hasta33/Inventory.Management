using FluentValidation;
using InventoryManagement.Core.DTOs.Model;

namespace InventoryManagement.Services.Validations.Model
{
    public class BrandUpdateDtoValidator : AbstractValidator<ModelUpdateDto>
    {
        public BrandUpdateDtoValidator() 
        {
            RuleFor(x => x.Id).NotNull().WithMessage("{PropertyName} bu alan gereklidir").NotEmpty().WithMessage("{PropertyName} bu alan gereklidir");
            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} bu alan gereklidir").NotEmpty().WithMessage("{PropertyName} bu alan gereklidir");
            RuleFor(x => x.BrandId).NotNull().WithMessage("{PropertyName} bu alan gereklidir").NotEmpty().WithMessage("{PropertyName} bu alan gereklidir");
        }
    }
}
