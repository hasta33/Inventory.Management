using FluentValidation;
using InventoryManagement.Core.DTOs.BrandModel.Model;

namespace InventoryManagement.Services.Validations.BrandModel.Model
{
    public class ModelCreateDtoValidator : AbstractValidator<ModelUpdateDto>
    {
        public ModelCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} bu alan gereklidir.").NotEmpty().WithMessage("{PropertyName} bu alan gereklidir.");
            RuleFor(x => x.BusinessCode).NotNull().WithMessage("{PropertyName} bu alan gereklidir.").NotEmpty().WithMessage("{PropertyName} bu alan gereklidir.");


            //Relationship
            RuleFor(x => x.BrandId).NotNull().WithMessage("{PropertyName} bu alan gereklidir.").NotEmpty().WithMessage("{PropertyName} bu alan gereklidir.");
        }
    }
}
