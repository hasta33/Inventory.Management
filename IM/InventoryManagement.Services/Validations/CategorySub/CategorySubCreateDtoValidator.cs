using FluentValidation;
using InventoryManagement.Core.DTOs.CategorySub;

namespace InventoryManagement.Services.Validations.CategorySub
{
    public class CategorySubCreateDtoValidator : AbstractValidator<CategorySubCreateDto>
    {
        public CategorySubCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} bu alan gereklidir.").NotEmpty().WithMessage("{PropertyName} bu alan gereklidir.");
        }
    }
}
