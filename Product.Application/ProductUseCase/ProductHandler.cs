using MediatR;
using Microsoft.Extensions.Logging;
using Product.Application.ProductUseCase.Request;
using Product.Application.ProductUseCase.Response;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.ProductUseCase
{
    public class ProductHandler : IRequestHandler<CreateProductRequest, CreateProductResponse>
    {
        private readonly ILogger<ProductHandler> _logger;

        public ProductHandler(ILogger<ProductHandler> logger)
        {
            _logger = logger;
        }

        public Task<CreateProductResponse> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
