using MediatR;
using Product.Application.Interfaces;
using Product.Application.ProductUseCase.Response;
using Product.Domain.Aggregates.ProductAgg;
using System;

namespace Product.Application.ProductUseCase.Request
{
    public record CreateProductRequest : IRequest<CreateProductResponse>, IUseCase
    {
        public string Name { get; set; }
        public string IMEI { get; set; }
        public string Model { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public int Memory { get; set; }
        public int Storage { get; set; }
        public int BatteryLifeInMinutes { get; set; }
        public string Manufacturer { get; set; }
        public Brand Brand { get; set; }
    }
}
