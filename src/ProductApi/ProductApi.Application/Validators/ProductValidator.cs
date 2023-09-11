using FluentValidation;
using ProductApi.Application.Dtos.Base;
using ProductApi.Domain.Interfaces;
using ProductApi.Domain.Models;

namespace ProductApi.Application.Validators
{
    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator(IBaseRepository<Product> repository) 
        {
            RuleFor(product => product.Name).NotEmpty().WithMessage("Product name is missing.");

            RuleSet("Create", () =>
            {
                RuleFor(product => product.Id).MustAsync(async (id, cancellation) =>
                {
                    var product = await repository.GetByIdAsync(id);

                    return product == null;
                }).WithMessage("A product with the same id already exists.");
            });
        }
    }
}
