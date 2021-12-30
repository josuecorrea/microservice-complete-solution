using FluentValidation;
using Product.Application.ProductUseCase.Request;

namespace Product.Application.ProductUseCase.Validator
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        public CreateProductRequestValidator()
        {

        }

    }
}
