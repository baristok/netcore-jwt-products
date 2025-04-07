using JwtApp.Back.Core.Application.Features.CQRS.Commands;
using JwtApp.Back.Core.Application.Interfaces;
using JwtApp.Back.Core.Domain;
using MediatR;

namespace JwtApp.Back.Core.Application.Features.CQRS.Handlers;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest>
{
    private readonly IRepository<Product> _productRepository;

    public CreateProductCommandHandler(IRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
        await this._productRepository.CreateAsync(new Product
        {
            CategoryId = request.CategoryId,
            Name = request.Name,
            Price = request.Price,
            Stock = request.Stock,
        });
    }
}