using FluentValidation;
using Homework.Services.Product.Domain.Models;

namespace Homework.Services.Product.Domain.Validators
{
    public class ProductDtoValidator:AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Price).NotNull().NotEmpty();
        }
    }
}