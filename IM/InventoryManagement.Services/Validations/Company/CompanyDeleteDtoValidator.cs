using FluentValidation;
using InventoryManagement.Core.DTOs.Company;

namespace InventoryManagement.Services.Validations.Company
{
    public class CompanyDeleteDtoValidator : AbstractValidator<CompanyDeleteDto>
    {
        public CompanyDeleteDtoValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("{PropertyName} bu alan gereklidir.").NotEmpty().WithMessage("{PropertyName} bu alan gereklidir.");
        }
    }
}
