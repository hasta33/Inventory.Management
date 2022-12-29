using FluentValidation;
using InventoryManagement.Core.DTOs.CategorySub;

namespace InventoryManagement.Services.Validations.CategorySub
{
    public class CategorySubDeleteDtoValidator : AbstractValidator<CategorySubDeleteDto>
    {
        public CategorySubDeleteDtoValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("{PropertyName} bu alan gereklidir.").NotEmpty().WithMessage("{PropertyName} bu alan gereklidir.");
        }
    }
}
